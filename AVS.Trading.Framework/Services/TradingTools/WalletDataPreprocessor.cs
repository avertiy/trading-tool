using System;
using System.Collections.Generic;
using System.Linq;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Core;
using AVS.Trading.Core.Interfaces;
using AVS.Trading.Core.Interfaces.LendingTools;
using AVS.Trading.Core.Interfaces.WalletTools;
using AVS.Trading.Data.Domain.WalletTools;

namespace AVS.Trading.Framework.Services.TradingTools
{
    public interface IWalletDataPreprocessor
    {
        Tuple<IBalance,IBalance> FilterOutBalances(IDictionary<string, IBalance> allBalances, CurrencyPair pair);

        BalanceInfo GetBalanceInfo(CurrencyPair pair, IDictionary<string, IBalance> balances,
            IDictionary<string, IDictionary<string, double>> tradeableBalances);

        IDictionary<string, IBalance> FilterOutZeroBalances(IDictionary<string, IBalance> balances);

        IList<ActiveLoan> PreprocessActiveLoans(IList<IActiveLoan> items);
    }
    public class WalletDataPreprocessor : IWalletDataPreprocessor
    {
        public Tuple<IBalance, IBalance> FilterOutBalances(IDictionary<string, IBalance> balances, CurrencyPair pair)
        {
            if (pair == null || pair == CurrencyPair.Any)
                return null;
            var res = new Tuple<IBalance, IBalance>(balances[pair.QuoteCurrency], balances[pair.BaseCurrency]);
            return res;
        }

        public BalanceInfo GetBalanceInfo(CurrencyPair pair, IDictionary<string, IBalance> balances, 
            IDictionary<string, IDictionary<string,double>> tradeableBalances)
        {
            if (pair == null || pair == CurrencyPair.Any)
                return null;
            var info = new BalanceInfo
            {
                QuoteCurrency = pair.QuoteCurrency,
                BaseCurrency = pair.BaseCurrency,
                QuoteAmount = balances[pair.QuoteCurrency].QuoteAvailable,
                QuoteAmountOnOrders = balances[pair.QuoteCurrency].QuoteOnOrders,
                BaseAmount = balances[pair.BaseCurrency].QuoteAvailable,
                BaseAmountOnOrders = balances[pair.BaseCurrency].QuoteOnOrders
            };
            var pairStr = pair.ToString();
            if (tradeableBalances!=null && tradeableBalances.ContainsKey(pairStr))
            {
                info.QuoteTradableAmount = tradeableBalances[pairStr][pair.QuoteCurrency];
                info.BaseTradableAmount = tradeableBalances[pairStr][pair.BaseCurrency];
            }
            return info;
        }

        public IDictionary<string, IBalance> FilterOutZeroBalances(IDictionary<string, IBalance> balances)
        {
            return balances.Where(kp => !kp.Value.IsEmpty).ToDictionary(kp=> kp.Key,kp=> kp.Value);
        }

        public IList<ActiveLoan> PreprocessActiveLoans(IList<IActiveLoan> items)
        {
            return items.Select(delegate(IActiveLoan i)
            {
                var loan = new ActiveLoan();
                i.Map(loan);
                return loan;
            }).ToList();
        }

    }
}