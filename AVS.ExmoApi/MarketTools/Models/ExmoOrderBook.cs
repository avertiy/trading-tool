using System.Collections.Generic;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Helpers;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Core.ResponseModels;
using Newtonsoft.Json;

namespace AVS.ExmoApi.MarketTools.Models
{
    /// <summary>
    /// represents OrderBook Exmo reponse
    /// </summary>
    /// <example>
    /// "BTC_USD": {
    ///    "ask_quantity": "3",
    ///    "ask_amount": "500",
    ///    "ask_top": "100",
    ///    "bid_quantity": "1",
    ///    "bid_amount": "99",
    ///    "bid_top": "99",
    ///    "ask": [[100,1,100],[200,2,400]],
    ///    "bid": [[99,1,99]]]
    ///  }
    /// </example>
    class ExmoOrderBook : PublicOrderBook, IPublicOrderBook
    {
        /// <summary>
        /// the sum of all quantity values in sell orders
        /// </summary>
        [JsonProperty("ask_quantity")]
        public double AskQuantity { get; set; }
        /// <summary>
        /// the sum of all total sum values in sell orders
        /// </summary>
        [JsonProperty("ask_amount")]
        public double AskAmount { get; set; }

        /// <summary>
        ///  minimum sell price
        /// </summary>
        [JsonProperty("ask_top")]
        public double AskTop { get; set; }
        
        [JsonProperty("bid_quantity")]
        public double BidQuantity { get; set; }
        [JsonProperty("bid_amount")]
        public double BidAmount { get; set; }
        [JsonProperty("bid_top")]
        public double BidTop { get; set; }

        [JsonProperty("bid")]
        private IList<string[]> BuyOrdersInternal
        {
            set => BuyOrders = ParseOrders(value);
        }

        [JsonProperty("ask")]
        private IList<string[]> SellOrdersInternal
        {
            set => SellOrders = ParseOrders(value);
        }

        private IList<IOrder> ParseOrders(IList<string[]> values)
        {
            var output = new List<IOrder>(values.Count);
            for (var i = 0; i < values.Count; i++)
            {
                var quantity = NumericHelper.ParseDouble(values[i][1]);
                var price = NumericHelper.ParseDouble(values[i][0]);                
                output.Add(new Order(price, quantity));
            }
            return output;
        }
    }
}