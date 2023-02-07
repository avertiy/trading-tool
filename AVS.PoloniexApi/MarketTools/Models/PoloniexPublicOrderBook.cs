using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Helpers;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Core.ResponseModels;
using Newtonsoft.Json;

namespace AVS.PoloniexApi.MarketTools.Models
{
    public class PoloniexPublicOrderBook : PublicOrderBook
    {
        [JsonProperty("bids")]
        private IList<string[]> BuyOrdersInternal {
            set => BuyOrders = ParseOrders(value);
        }

        [JsonProperty("asks")]
        private IList<string[]> SellOrdersInternal {
            set => SellOrders = ParseOrders(value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static IList<IOrder> ParseOrders(IList<string[]> orders)
        {
            var output = new List<IOrder>(orders.Count);
            for (var i = 0; i < orders.Count; i++)
            {
                output.Add(
                    new Order(
                        NumericHelper.ParseDouble(orders[i][0]),
                        NumericHelper.ParseDouble(orders[i][1])
                    )
                );
            }
            return output;
        }
    }
}
