using System;
using System.Collections.Generic;
using System.Linq;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.ResponseModels;
using AVS.Trading.Core.ResponseModels.JsonConverters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AVS.PoloniexApi.TradingTools.Models
{
    /// <summary>
    /// Represents JSON
    /// { 
    /// "success": 1, "message": "Margin order placed.", "orderNumber": "154407998", 
    /// "resultingTrades": {
    ///             "BTC_DASH": [ 
    ///              {
    ///                  "amount": "1.00000000",
    ///                  "date": "2015-05-10 22:47:05",
    ///                  "rate": "0.01383692",
    ///                  "total": "0.01383692",
    ///                  "tradeID": "1213556",
    ///                  "type": "buy"
    ///              }] 
    ///     }
    /// }
    /// </summary>
    [JsonConverter(typeof(PlaceOrderResultJsonConverter))]
    public class PlaceOrderResult: SimpleResponse, IPlaceOrderResult
    {
        public string OrderNumber { get; set; }
        public string Market { get; set; }
        public IList<ITrade> Trades { get; set; }
    }

    public class PlaceOrderResultJsonConverter : BaseConverter
    {
        protected override object Parse(JObject jObject, Type objectType, JsonSerializer serializer)
        {
            var instance = (PlaceOrderResult)CreateInstance(jObject, objectType);
            if (instance.Success)
            {
                instance.OrderNumber = jObject["orderNumber"].Value<string>();
                if (jObject["resultingTrades"] is JObject resultingTrades)
                {
                    var property = resultingTrades.Properties().FirstOrDefault();
                    if (property != null)
                    {
                        instance.Market = property.Name;
                        instance.Trades = property.Value.ToObject<List<Trade>>().ToList<ITrade>();
                    }
                }
            }

            return instance;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(PlaceOrderResult).IsAssignableFrom(objectType);
        }
    }
}