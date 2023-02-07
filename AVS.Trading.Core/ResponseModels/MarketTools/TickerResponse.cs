using System.Collections.Generic;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Interfaces.MarketTools;

namespace AVS.Trading.Core.ResponseModels.MarketTools
{
    public class TickerResponse: Response<IDictionary<string,IMarketData>>
    {
    }

    public class MarketTradeHistory : Response<IList<IMarketTrade>>
    {
    }

    public class ChartData : Response<IList<ICandlestick>>
    {
    }
}