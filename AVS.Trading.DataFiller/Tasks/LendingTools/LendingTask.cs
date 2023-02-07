using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVS.CoreLib.Data.Domain.Tasks;
using AVS.CoreLib.Services.Logging.LogWriters;
using AVS.CoreLib.Services.Tasks;
using AVS.Trading.DataFiller.Tasks.MarketTools;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Services.LendingTools;
using AVS.Trading.Framework.Utils;
using AVS.Trading.Core;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.ResponseModels.TradingTools;
using AVS.Trading.Framework.Tasks;

namespace AVS.Trading.DataFiller.Tasks.LendingTools
{
    public class LendingTask : TaskBase
    {
        private readonly IBalanceHelper _balanceHelper;
        private readonly ILendingToolsService _lendingToolsService;
        private readonly ILendingContextAnalizer _lendingContextAnalizer;

        public LendingTask(TradingAppConfig config, IWorkContext workContext, ExchangeDirectory exchangeDirectory, IBalanceHelper balanceHelper, ILendingToolsService lendingToolsService, ILendingContextAnalizer lendingContextAnalizer) : base(config, workContext, exchangeDirectory)
        {
            _balanceHelper = balanceHelper;
            _lendingToolsService = lendingToolsService;
            _lendingContextAnalizer = lendingContextAnalizer;
        }

        public static ScheduleTask DefaultScheduleTask
        {
            get
            {
                var task = new ScheduleTask
                {
                    Name = "Lending Task",
                    Description = "Watches lending market and manages loan offers",
                    Seconds = 20,//60 * 5,
                    Enabled = false,
                    StopOnError = false,
                    Type = typeof(LoadChartDataTask).AssemblyQualifiedName
                };
                return task;
            }
        }

        public override void Execute(TaskLogWriter log, TaskParameters parameters)
        {
            this.ForEachExchange((client) =>
            {
                if (!CheckRequiredTools(client))
                {
                    log.Write($"{client.Exchange} does not support required tools");
                    return;
                }

                this.ForEachPair(pair => { Execute(log, pair); });
            });
        }

        protected void Execute(TaskLogWriter log, PairString pair)
        {
            //1. выбрать монетку с наилучшим (бОльшим) лоан рейтом, причём лоан рейт >= 0.014% в сутки
            var lendingMarket = GetBestLendingMarket(_lendingContextAnalizer.MinLendingRate);

            if (lendingMarket == null)
            {
                log.Write("No good loan opportunities found");
                return;
            }

            _balanceHelper.Initialize();

            if (_balanceHelper.Balances == null)
            {
                log.WriteFail($"Unable to get available balances", _balanceHelper.Error);
                return;
            }

            var ctx = CreateLendingContext(lendingMarket);

            if (!_lendingContextAnalizer.ShouldPlaceLoanOffer(ctx))
            {
                log.Write("No opportunities detected to place loan offer");
                return;
            }
            
            if (!ArrangeLendingAmount(ctx, ctx.MinLendingAmount, ctx.TargetRate > 0.04))
            {
                log.WriteFail($"Unable to arrange {ctx.MinLendingAmount} {ctx.TargetCurrency} to place loan offer at {ctx.TargetRate:P} rate",
                        _balanceHelper.Error);
                return;
            }

            PlaceLoanOffer(log, ctx.TargetCurrency, ctx.MinLendingAmount, ctx.TargetRate, ctx.Duration, true);
        }

        private LendingContext CreateLendingContext(ILoanOrders lendingMarket)
        {
            var allOffers = _lendingToolsService.GetOpenLoanOffers();
            return new LendingContext()
            {
                TargetCurrency = lendingMarket.Currency,
                MarketLoanOffers = lendingMarket.Offers,
                MarketLoanDemands = lendingMarket.Demands,
                Balances = _balanceHelper.Balances,
                AllOffers = allOffers.Data,
                MinLendingAmount = _lendingToolsService.GetMinLendingAmount(lendingMarket.Currency),
                MinLendingRate = _lendingContextAnalizer.MinLendingRate,
                ProvidedLoans = _lendingToolsService.GetProvidedActiveLoans(lendingMarket.Currency),
            };
        }

        /// <summary>
        /// Arranges the required amount to be available on a lending account
        /// </summary>
        /// <param name="ctx"></param>
        /// <param name="amount">required amount to be available</param>
        /// <param name="rebalanceFunds">
        /// when true it might try to cancel loan offers for other currencies, transfers their balances to margin account to arrange the required amount
        /// </param>
        /// <returns>true if arraning is success</returns>
        private bool ArrangeLendingAmount(LendingContext ctx, double amount, bool rebalanceFunds)
        {
            bool result = _balanceHelper.ArrangeAmount(ctx.TargetCurrency, amount, AccountType.Lending);

            if (!result && rebalanceFunds)
            {
                _balanceHelper.FreeUpFunds(ctx);
                result = _balanceHelper.ArrangeAmount(ctx.TargetCurrency, amount, AccountType.Lending);
            }

            return result;
        }

        private void PlaceLoanOffer(TaskLogWriter log, string currency, double amount, double rate, int duration,
            bool autorenew = true)
        {

            //Execute(log,()=>{.. return response},$"error message {asdsad}" [,$"success message"]) => true/false;
            //log.Run(()=>{.. return response},$"error message {asdsad}" [,$"success message"]) => true/false;

            var response = _lendingToolsService.CreateLoanOffer(currency, amount, rate, duration, autorenew);

            var offerStr =
                $"{rate:P}% {amount:0.00} {currency} for {duration} days autorenew:{(autorenew ? "On" : "Off")}";

            if (!response.Success)
            {
                log.WriteFail($"Unable to place loan offer: {offerStr}", response.Error);
            }
            else
            {
                log.Write($"Placed loan offer {offerStr}");
            }
        }

        protected bool CheckRequiredTools(ExchangeClient client)
        {
            return client.LendingTools != null && client.WalletTools != null && client.MarginTools != null;
        }

        private ILoanOrders GetBestLendingMarket(double minLendingRate)
        {
            var currencies = WorkContext.Client.Pairs.GetCoinsFor(AccountType.Lending);
            double bestRate = minLendingRate;
            ILoanOrders bestCoin = null;

            foreach (var currency in currencies)
            {
                var response = _lendingToolsService.GetMarketLoanOrders(currency);

                double smallLoanOrdersThreshold = response.Data.Offers.Average(o => o.Amount) / 3;

                //detect top walls
                var walls = response.Data.Offers.Where(o => o.Amount >= smallLoanOrdersThreshold).Take(5).ToArray();

                var rate = walls.First(o => o.Rate >= bestRate).Rate;
                if (rate > bestRate)
                {
                    bestRate = rate;
                    bestCoin = response.Data;
                }
            }

            return bestCoin;
        }

        
    }
}
