using System;
using System.Linq;
using AVS.CoreLib.Services.Logging.LogWriters;
using AVS.Trading.Framework.Services.WalletTools;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.ResponseModels;
using AVS.Trading.Core.ResponseModels.TradingTools;
using AVS.Trading.Core.Services;

namespace AVS.Trading.Framework.Services.LendingTools
{
    public interface IBalanceHelper
    {
        string Error { get; }
        IAvailableAccountBalances Balances { get; }
        bool RefreshBalances();
        void Initialize();
        bool ArrangeAmount(string currency, double amount, AccountType account);
        void FreeUpFunds(LendingContext ctx);
    }


    public class BalanceHelper: IBalanceHelper
    {
        public double MinMarginValue = 0.44;
        private readonly IWalletToolsService _walletToolsService;
        private readonly IMarginToolsService _marginToolsService;
        private readonly ILendingToolsService _lendingToolsService;

        public IAvailableAccountBalances Balances { get; protected set; }

        public string Error { get; protected set; }

        public BalanceHelper(IWalletToolsService walletToolsService, IMarginToolsService marginToolsService, ILendingToolsService lendingToolsService)
        {
            _walletToolsService = walletToolsService;
            _marginToolsService = marginToolsService;
            _lendingToolsService = lendingToolsService;
        }

        public void Initialize()
        {
            RefreshBalances();
            Error = null;
        }

        public bool TransferBalance(string currency, double amount, AccountType @from, AccountType to)
        {
            SimpleResponse response =
                _walletToolsService.TransferBalance(currency, amount, @from, to);
            if (false == response.Success)
            {
                Error = response.Error;
                return false;
            }

            return true;
        }

        public bool ArrangeAmount(string currency, double amount, AccountType account)
        {
            if (account == AccountType.Lending)
                return ArrangeLendingBalance(currency, amount);

            throw new NotImplementedException();
        }

        private bool ArrangeLendingBalance(string currency, double amount)
        {
            if (Balances == null)
                if (!RefreshBalances())
                    return false;

            CheckLendingBalance:

            var lendingBalance = Balances.Lending[currency];

            if (lendingBalance >= amount)
                return true;

            var exchangeBalance = Balances.Exchange[currency];
            if (exchangeBalance > 0)
            {
                var transferAmount = exchangeBalance;
                if (exchangeBalance > amount)
                {
                    transferAmount = amount;
                }

                if (TransferBalance(currency, transferAmount, AccountType.Exchange, AccountType.Lending))
                {
                    RefreshBalances();
                    goto CheckLendingBalance;
                }
            }

            var response = _marginToolsService.GetMarginAccountSummary();
            if (response.Success)
            {
                var summary = response.Data;
                var marginBalance = Balances.Margin[currency];
                if (marginBalance > amount && summary.CurrentMargin > MinMarginValue)
                {
                    if (TransferBalance(currency, amount, AccountType.Margin, AccountType.Lending))
                    {
                        RefreshBalances();
                        goto CheckLendingBalance;
                    }
                }
            }

            return false;
        }

        public bool RefreshBalances()
        {
            var response = _marginToolsService.GetAvailableAccountBalances();
            if (false == response.Success)
            {
                Error = response.Error;
                return false;
            }
            Balances = response.Data;
            return true;
        }

        public void FreeUpFunds(LendingContext ctx)
        {
            foreach (var currency in ctx.AllOffers.Keys)
            {
                if (currency == ctx.TargetCurrency)
                    continue;
                //закенселить открытые лоан офферы, если их больше одного (высвободить средства)
                CancelOffers(ctx, currency, ctx.MinLendingRate, false);
            }

            RefreshBalances();

            //transfer freed up funds from lending to margin account
            foreach (var kp in Balances.Lending)
            {
                if (kp.Key == ctx.TargetCurrency)
                    continue;

                TransferBalance(kp.Key, kp.Value, AccountType.Lending, AccountType.Margin);
            }
        }

        private void CancelOffers(LendingContext ctx, string currency, double minLendingRate, bool cancelAll)
        {
            if (ctx.AllOffers.ContainsKey(currency) && ctx.AllOffers[currency].Count > 1)
            {
                var offers = ctx.AllOffers[currency];
                bool keepOneOffer = false;
                foreach (var offer in offers.OrderBy(o => o.Rate))
                {
                    if (cancelAll || keepOneOffer || offer.Rate < minLendingRate)
                    {
                        var cancelResponse = _lendingToolsService.CancelLoanOffer(offer.Id);
                        if (!cancelResponse.Success)
                        {
                            Error = cancelResponse.Error;
                            continue;
                        }
                    }

                    keepOneOffer = true;
                }
            }
        }

    }
}