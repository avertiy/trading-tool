using System.Collections.Generic;
using AVS.CoreLib.Json.Converters;
using AVS.Trading.Core.ResponseModels.MarketTools;
using Newtonsoft.Json;

namespace AVS.BinanceApi.MarketTools.Models
{
    // Если обьем > 0 - BID.
    // Если обьем< 0 - ASK.
    public class BinanceOrderBook : OrderBook
    {
        public static BinanceOrderBook From(IList<BinanceOrderBookEntry> records)
        {
            var book = new BinanceOrderBook();
            book.Initialize(records);
            return book;
        }

        private void Initialize(IEnumerable<BinanceOrderBookEntry> records)
        {
            // if Quantity > 0 - BID, otherwise  - ASK.
            foreach (var record in records)
            {
                if (record.Quantity > 0)
                    AddBuyOrder(record.Price, record.Quantity);
                else
                    AddSellOrder(record.Price, record.Quantity * -1);
            }
        }
    }

    //orderbook: [[262477.0,0.009708,1],[262476.0,0.017731,1],[262474.0,0.038105,1],[262201.0,0.000804,1],[262200.0,0.061952,2]]
    [JsonConverter(typeof(ArrayConverter))]
    public class BinanceOrderBookEntry
    {
        [ArrayProperty(0)]
        public double Price { get; set; }
        [ArrayProperty(1)]
        public double Quantity { get; set; }
        [ArrayProperty(2)]
        public int Count { get; set; }
    }
}