using System.Collections.Generic;
using System.Threading.Tasks;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.ResponseModels.TradingTools;

namespace AVS.Trading.Core.Interfaces.TradingTools
{
    public interface IMarginTradingApi
    {
        Response<IMarginPosition> GetMarginPosition(CurrencyPair pair);
        Response<IList<IMarginPosition>> GetMarginPositions();
        Response<IMarginAccountSummary> GetMarginAccountSummary();
        IPlaceOrderResult PostMarginOrder(CurrencyPair currencyPair, double pricePerCoin,double amountQuote, OrderType type);
        IDictionary<string, IDictionary<string, double>> GetTradableBalances();
        Response<IAvailableAccountBalances> GetAvailableAccountBalances();

        //async methods
        Task<Response<IPlaceOrderResult>> PostMarginOrderAsync(OrderType type, string pair,
            double pricePerCoin, double amountQuote, double lendingRate);
    }
}