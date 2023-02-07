using System;
using System.Collections;
using System.Linq;
using AVS.CoreLib.Extensions;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AVS.Trading.Core.ResponseModels.JsonConverters
{
    public class MarketsResponseConverter : BaseConverter
    {
        protected override object Parse(JObject jObject, Type objectType, JsonSerializer serializer)
        {
            var instance = CreateInstance(jObject, objectType);
            
            var responseType = objectType.FindGenericType("MarketsResponse") ?? objectType.FindGenericType("MarketsListResponse");
            if (responseType == null)
                throw new ArgumentException($"objectType is expected to be MarketsResponse<T> or MarketsListResponse<T>");

            var tType = responseType.GetGenericArguments().First();

            var addMethod = responseType.GetMethod("Add", new[] { typeof(string), tType });
            if (addMethod == null)
                throw new Exception("Add(key, value) method was not found");

            var properties = jObject.Properties();
            foreach (var property in properties)
            {
                if (jObject[property.Name].Type == JTokenType.Object)
                {
                    var rdr = jObject[property.Name].CreateReader();
                    var tValue = serializer.Deserialize(rdr, tType);
                    addMethod.Invoke(instance, new object[] { property.Name, tValue });
                }
            }

            return instance;
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IDictionary).IsAssignableFrom(objectType);
        }
    }
}