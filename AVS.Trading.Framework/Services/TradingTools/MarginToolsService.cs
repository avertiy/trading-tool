using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AVS.CoreLib._System.Net;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.ResponseModels.TradingTools;
using AVS.Trading.Core.Services;

namespace AVS.Trading.Framework.Services.TradingTools
{
    public class MarginToolsService : IMarginToolsService
    {
        private readonly IWorkContext _workContext;

        public MarginToolsService(IWorkContext workContext)
        {
            _workContext = workContext;
        }

        protected ExchangeClient Client => _workContext.Client;
        protected IMarginTradingApi MarginTools => Client.MarginTools?? throw new InvalidOperationException($"{Client.Exchange} does not support MarginTools");
        
        #region MarginTrading

        /// <summary>
        /// Returns your current tradable balances for each currency in each market for which margin trading is enabled. 
        /// </summary>
        /// <returns>{"BTC_DASH":{"BTC":"8.50274777","DASH":"654.05752077"}}</returns>
        [DebuggerStepThrough]
        public IDictionary<string, IDictionary<string, double>> GetTradableBalances()
        {
            return MarginTools.GetTradableBalances();
        }

        [DebuggerStepThrough]
        public Response<IMarginAccountSummary> GetMarginAccountSummary()
        {
            return MarginTools.GetMarginAccountSummary();
        }
        [DebuggerStepThrough]
        public Response<IMarginPosition> GetMarginPosition(CurrencyPair pair)
        {
            return MarginTools.GetMarginPosition(pair);
        }
        [DebuggerStepThrough]
        public Response<IList<IMarginPosition>> GetMarginPositions()
        {
            return MarginTools.GetMarginPositions();
        }
        //[DebuggerStepThrough]
        public IPlaceOrderResult MarginBuy(CurrencyPair currencyPair, double pricePerCoin, double amountQuote)
        {
            return MarginTools.PostMarginOrder(currencyPair, pricePerCoin, amountQuote, OrderType.Buy);
        }
        [DebuggerStepThrough]
        public IPlaceOrderResult MarginSell(CurrencyPair currencyPair, double pricePerCoin, double amountQuote)
        {
            return MarginTools.PostMarginOrder(currencyPair, pricePerCoin, amountQuote, OrderType.Sell);
        }

        public Response<IAvailableAccountBalances> GetAvailableAccountBalances()
        {
            return MarginTools.GetAvailableAccountBalances();
        }

        #endregion
        [DebuggerStepThrough]
        public Task<Response<IPlaceOrderResult>> SubmitMarginOrderAsync(string market, OrderType type,
            double pricePerCoin, double amountQuote, double loanRate)
        {
            string pair = Client.Pairs.GetPair(market);
            return MarginTools.PostMarginOrderAsync(type, pair, pricePerCoin, amountQuote, loanRate);
        }
    }
}