using System.Collections.Generic;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.ResponseModels;
using Newtonsoft.Json;

namespace AVS.ExmoApi.TradingTools.Models
{
    /*
    {
    "type": "buy",
    "in_currency": "BTC",
    "in_amount": "1",
    "out_currency": "USD",
    "out_amount": "100",
    "trades": [
    {
        "trade_id": 3,
        "date": 1435488248,
        "type": "buy",
        "pair": "BTC_USD",
        "order_id": 12345,
        "quantity": 1,
        "price": 100,
        "amount": 100
    }
    ]
}*/


    //public class OrderTradesResponse: SimpleResponse
    //{
    //    private IList<Trade> Trades;
    //}

    public class PostOrder : Response, IPostOrderResult
    {
        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonProperty("order_id")]
        public string IdOrder { get; set; }
    }
}