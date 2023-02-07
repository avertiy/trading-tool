using System.Collections.Generic;
using System.Linq;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.ResponseModels.JsonConverters;
using Newtonsoft.Json;

namespace AVS.Trading.Core.ResponseModels
{
    public interface IMarket
    {
        string Market { get; set; }
    }
    [JsonConverter(typeof(MarketsResponseConverter))]
    public class MarketsResponse<T> : Response
    {
        public MarketsResponse()
        {
            Items = new Dictionary<string, T>();
        }

        public IDictionary<string, T> Items { get; set; }

        public IList<T> ToList()
        {
            return Items.Values.ToList();
        }

        public void Add(string market, T value)
        {
            if (value is IMarket imarket)
                imarket.Market = market;
            Items.Add(market,value);
        }
    }

    [JsonConverter(typeof(MarketsResponseConverter))]
    public class MarketsListResponse<T> : Response
        where T: IMarket
    {
        public MarketsListResponse()
        {
            Items = new List<T>();
        }

        public IList<T> Items { get; set; }

        public void Add(string market, T value)
        {
            if (value is IMarket imarket)
                imarket.Market = market;
            Items.Add(value);
        }
    }
}