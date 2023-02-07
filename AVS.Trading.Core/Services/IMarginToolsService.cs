using System.Collections.Generic;
using System.Threading.Tasks;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.ResponseModels.TradingTools;

namespace AVS.Trading.Core.Services
{
    public interface IMarginToolsService
    {
        #region MarginTrading tools
        IDictionary<string, IDictionary<string, double>> GetTradableBalances();
        Response<IMarginPosition> GetMarginPosition(CurrencyPair pair);
        Response<IList<IMarginPosition>> GetMarginPositions();
        Response<IMarginAccountSummary> GetMarginAccountSummary();
        IPlaceOrderResult MarginBuy(CurrencyPair currencyPair, double pricePerCoin, double amountQuote);
        IPlaceOrderResult MarginSell(CurrencyPair currencyPair, double pricePerCoin, double amountQuote);
        Response<IAvailableAccountBalances> GetAvailableAccountBalances();
        #endregion

        //async methods
        Task<Response<IPlaceOrderResult>> SubmitMarginOrderAsync(string market, OrderType type,
            double pricePerCoin, double amountQuote, double loanRate);
    }
}