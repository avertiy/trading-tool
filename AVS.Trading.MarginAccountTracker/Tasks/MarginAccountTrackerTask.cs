using System;
using System.Collections.Generic;
using System.Linq;
using AVS.CoreLib.Data.Domain.Logging;
using AVS.CoreLib.Data.Domain.Tasks;
using AVS.CoreLib.Services.Logging.LogWriters;
using AVS.Trading.Framework.Adapters;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Services.TradingTools;
using AVS.Trading.Framework.Services.WalletTools;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.Interfaces.WalletTools;
using AVS.Trading.Core.ResponseModels;
using AVS.Trading.Core.Services;
using AVS.Trading.Framework.Services.MarketTools;
using AVS.Trading.Framework.Tasks;
using AVS.Trading.Framework.Utils;

namespace AVS.Trading.MarginAccountTracker.Tasks
{
    public class MarginAccountTrackerTask : TaskBase
    {
        private const double SafeThreshold = 0.31;
        private const double LowThreshold = 0.265;
        private const double CriticalThreshold = 0.24;
        private readonly MarketToolsDataAdapter _marketToolsDataAdapter;
        private readonly IMarketToolsService _marketToolsService;

        private readonly IWalletToolsService _walletTools;
        private readonly ITradingToolsService _tradingToolsService;
        private readonly IMarginToolsService _marginToolsService;
        private readonly IWalletDataPreprocessor _dataPreprocessor;
        private static DateTime? _lastBalanceCheck = null;
        
        public MarginAccountTrackerTask(TradingAppConfig config, IWorkContext workContext,
            ExchangeDirectory exchangeDirectory,
            MarketToolsDataAdapter marketToolsDataAdapter, 
            IWalletToolsService walletTools, ITradingToolsService tradingToolsService, 
            IMarginToolsService marginToolsService, IWalletDataPreprocessor dataPreprocessor,
            IMarketToolsService marketToolsService) : base(config, workContext, exchangeDirectory)
        {
            _marketToolsDataAdapter = marketToolsDataAdapter;
            _walletTools = walletTools;
            _tradingToolsService = tradingToolsService;
            _marginToolsService = marginToolsService;
            _dataPreprocessor = dataPreprocessor;
            _marketToolsService = marketToolsService;
        }

        public static ScheduleTask DefaultScheduleTask
        {
            get
            {
                var task = new ScheduleTask
                {
                    Name = "Margin account tracker",
                    Description = $"watches collateral level",
                    Group = "Margin Tools",
                    Seconds = 60,
                    Enabled = true,
                    StopOnError = false,
                    Type = typeof(MarginAccountTrackerTask).AssemblyQualifiedName
                };
                return task;
            }
        }

        private CollateralThreshold _thresholds;

        public override void Execute(TaskLogWriter log, TaskParameters parameters)
        {
            _thresholds.Safe = parameters.GetDouble("safe", SafeThreshold);
            _thresholds.Low = parameters.GetDouble("low", LowThreshold);
            _thresholds.Critical = parameters.GetDouble("critical", CriticalThreshold);

            this.ForEachExchange((client) =>
            {
                if (client.MarginTools == null)
                    return;

                this.ForEachAccount(client.Exchange, () =>
                {
                    log.Write($"Running margin tracker for '{client.Account.Name}' account on {client.Exchange}");
                    log.Write($"thresholds: {_thresholds}");
                    CheckCurrentMargin(log);
                });
            });
        }

        private void CheckCurrentMargin(TaskLogWriter log)
        {
            checkSummary:
            var response = _marginToolsService.GetMarginAccountSummary();
            if (!response.Success)
            {
                log.Write("Unable to load margin summary\r\n " + response.Error);
                return;
            }

            var summary = response.Data;

            if (summary.CurrentMargin > _thresholds.Safe)
            {
                log.Write($"Current margin [{summary.CurrentMargin * 100:N2}%] is OK");
            }
            else
            {
                if (summary.CurrentMargin < _thresholds.Low)
                {
                    log.Write($"WARNING current margin {summary.CurrentMargin * 100:N2}% is LOW");
                    bool critical = summary.CurrentMargin < _thresholds.Critical;
                    ShrinkMarginPosition(log, critical);
                    goto checkSummary;
                }

                log.Write($"Current margin {summary.CurrentMargin * 100:N2}% is Safe");

                if (_lastBalanceCheck == null || _lastBalanceCheck.Value < DateTime.Today)
                {
                    _lastBalanceCheck = DateTime.Now;
                    var balances = _walletTools.GetCompleteBalances().Data;
                    balances = _dataPreprocessor.FilterOutZeroBalances(balances);

                    if (TransferAvailableBalances(log, balances))
                        return;

                    if (TransferOpenOrdersBalances(log, balances))
                        return;

                    //sell not implemented yet
                    if (summary.CurrentMargin < _thresholds.Low)
                    {
                        log.Write(LogLevel.WARNING, $"WARNING current margin {summary.CurrentMargin * 100:N2}% is LOW", null);
                        //SellCoinsAndTransferBalances(log, balances);
                    }
                }
            }
        }
        
        private void ShrinkMarginPosition(TaskLogWriter log, bool critical)
        {
            var response = _marginToolsService.GetMarginPositions();
            if (!response.Success)
            {
                log.WriteFail($"GetMarginPositions failed", response.Error);
                return;
            }

            var positions = response.Data.OrderBy(p => p.ProfitLoss);
            foreach (var position in positions.Where(p => p.LiquidationPrice > 0))
            {
                var price = _marketToolsDataAdapter.GetLastPrice(position.Market);
                var priceDistance = PriceDistance(price, position.LiquidationPrice);
                if (!(priceDistance < 17))
                {
                    if (priceDistance < 50)
                        log.Write($"{position.Market} liquidation price distance {priceDistance:N2}% is OK");
                    continue;
                }

                log.Write(LogLevel.WARNING, 
                    $"{position.Market} liquidation price distance is too low {priceDistance:N2}%. Shrink position is triggered", null);

                var k = 1500;
                if (critical)
                    k = 100;
                var shrinkAmount = position.Amount / k; 
                if (shrinkAmount < 0)
                    shrinkAmount = shrinkAmount * -1;

                var bestMarketPrice = price;

                //shrink position
                IPlaceOrderResult result;
                if (position.Type == PositionType.Long)
                {
                    result = _marginToolsService.MarginSell(CurrencyPair.Parse(position.Market), bestMarketPrice,
                        shrinkAmount);
                }
                else
                {
                    result = _marginToolsService.MarginBuy(CurrencyPair.Parse(position.Market), bestMarketPrice,
                        shrinkAmount);
                }
                if (result != null)
                    log.Write(result.Message);
            }
        }

        private bool TransferAvailableBalances(TaskLogWriter log, IDictionary<string, IBalance> balances)
        {
            var arr = new string[] { "BTC", "DASH", "DOGE", "ETH", "LTC", "STR", "XMR", "XRP" };
            //try available balances
            var availableQuoteBalances = balances.Where(kp => arr.Contains(kp.Key) && kp.Value.QuoteAvailable > 0).ToArray();
            if (availableQuoteBalances.Any())
            {
                foreach (var balance in availableQuoteBalances)
                {
                    if (TransferBalance(log, balance.Key, balance.Value.QuoteAvailable))
                        return true;
                }
            }
            return false;
        }

        private bool TransferOpenOrdersBalances(TaskLogWriter log, IDictionary<string, IBalance> balances)
        {
            //cancel open orders on exchange and transfer balances
            var balancesOnOrders = balances.Where(kp => kp.Value.QuoteOnOrders > 0).ToArray();
            if (balancesOnOrders.Any())
            {
                //1. process coins that match Poloniex margin markets
                //1.1 Release coins canceling open orders
                //1.2 Transfer coins to Margin Account
                //so we can do transfer balance once funds are released from orders
                foreach (var balance in balancesOnOrders.Where(b => Constants.PoloniexMarginCurrencies.Contains(b.Key)))
                {
                    var amount = CancelOpenOrders(log, balance.Key);
                    if (amount < 0.0000001)
                        continue;
                    if (TransferBalance(log, balance.Key, amount))
                        return true;
                }
            }

            return false;
        }

        private bool SellCoinsAndTransferBalances(TaskLogWriter log, IDictionary<string, IBalance> balances)
        {
            var pair = "BTC_MAID";
            double amount = 75.0;

            //sell portion of MAID
            var orderBook = _marketToolsService.LoadOrderBook(pair);
            if (!orderBook.Success)
                return false;

            var price = orderBook.Data.BuyOrders.First().Price;
            
            var placeOrderResult =_tradingToolsService.PostOrder(pair, OrderType.Sell, price, amount);
            if (placeOrderResult.Success)
            {
                log.Write($"Sold {amount} {pair}");
                var response = _walletTools.GetCompleteBalances();
                if (!response.Success)
                {
                    log.WriteFail($"GetCompleteBalances failed", response.Error);
                    return false;
                }

                var btcBalance = response.Data["BTC"];

                if (btcBalance.IsEmpty)
                {
                    log.Write($"BTC balance is empty that's weird at this point");
                    throw new Exception($"BTC balance is empty that's weird at this point");
                }

                if (TransferBalance(log, "BTC", btcBalance.QuoteAvailable))
                    return true;
            }


            return false;


            

            ////1. cancel orders
            ////2. sell coins get BTC
            ////3 transfer BTC funds to Margin account
            ////to do later due to it forces us to close losses
            //foreach (var balance in balances.Where(b => b.Value.QuoteOnOrders > 0 && !Constants.PoloniexMarginCurrencies.Contains(b.Key)))
            //{
            //    var amount = CancelOpenOrders(log, balance.Key);
            //    if (amount < 0.0000001)
            //        continue;

            //    if (PanicSell(log, balance.Key, amount))
            //    {
            //        var response = _walletTools.GetCompleteBalances();
            //        if (!response.Success)
            //        {
            //            log.WriteFail($"GetCompleteBalances failed", response.Error);
            //            continue;
            //        }

            //        var btcBalance = response.Data["BTC"];

            //        if (btcBalance.IsEmpty)
            //        {
            //            log.Write($"BTC balance is empty that's weird at this point");
            //            throw new Exception($"BTC balance is empty that's weird at this point");
            //        }

            //        if (TransferBalance(log, "BTC", btcBalance.QuoteAvailable))
            //            return true;
            //    }
            //}

            //return false;
        }

        private double PriceDistance(double price1, double price2)
        {
            if (price1 < price2)
                return (price2 - price1) / price1 * 100;
            return (price1 - price2) / price1 * 100;
        }

        private bool PanicSell(TaskLogWriter log, string balanceKey, double amount)
        {
            throw new NotImplementedException();
        }

        private double CancelOpenOrders(TaskLogWriter log, string currency)
        {
            double amount = 0;
            var baseCurrencies = new[] {"USDT", "USDC", "ETH", "BTC" };

            foreach (var baseCurrency in baseCurrencies)
            {
                var pair = new CurrencyPair(baseCurrency, currency);

                var response = _tradingToolsService.GetOpenOrders(pair.ToString());
                if(!response.Success)
                    continue;

                IList<ILimitOrder> openOrders = response.Data;
                openOrders = openOrders.Where(o => o.Account == TradingAccount.Exchange).ToList();
                if (openOrders.Any())
                {
                    foreach (ILimitOrder order in openOrders)
                    {
                        if (_tradingToolsService.CancelOrder(pair.ToString(), order.IdOrder))
                        {
                            log.Write($"Canceled {order.Type} order {order.AmountQuote} {currency} x {order.Price}");
                            amount += order.AmountBase;
                            //cancel not all orders but 1 or a few at a time;
                            if (baseCurrency.Either("USDT", "USDC") && amount > 10)
                                return amount;
                            if (baseCurrency.Either("BTC", "ETH") && amount > 0.01)
                                return amount;
                        }
                    }
                }
            } 
            return amount;
        }

        private bool TransferBalance(TaskLogWriter log, string currency, double amount)
        {
            SimpleResponse result = _walletTools.TransferBalance(currency, amount, AccountType.Exchange, AccountType.Margin);
            log.Write($"Transfer balance Exchange->Margin {amount.FormatNumber(currency)} - " + result.Message);
            if (result.Success)
            {
                var response = _marginToolsService.GetMarginAccountSummary();
                if (!response.Success)
                    return false;
                var summary = response.Data;
                if (summary.CurrentMargin > _thresholds.Low)
                    return true;
            }
            return false;
        }
    }

    struct CollateralThreshold
    {
        public double Safe;
        public double Low;
        public double Critical;

        public override string ToString()
        {
            return $"{Safe*100}% - safe; {Low*100}% - low; {Critical*100}% - critical";
        }
    }

    
}