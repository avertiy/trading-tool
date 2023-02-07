using System;
using System.Linq;
using AVS.CoreLib.Extensions;
using AVS.CoreLib._System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AVS.Trading.Core.ResponseModels.JsonConverters
{
    public abstract class BaseConverter : JsonConverter
    {
        public override bool CanWrite => false;
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            JToken token = JToken.Load(reader);
            object result = null;
            if (token.Type == JTokenType.Object)
            {
                result = Parse((JObject)token, objectType, serializer);
            }

            if(token.Type == JTokenType.Array)
            {
                result = Parse((JArray) token, objectType, serializer);
            }
            return result;
        }
        
        protected void InitializeSimpleResponse(SimpleResponse model, JObject jObject)
        {
            model.Message = jObject["message"]?.Value<string>();
            var success = jObject["success"];
            if(success!=null)
                model.SuccessIntValue = success.Value<int>();
        }

        protected virtual object CreateInstance(JObject jObject, Type objectType)
        {
            var instance = Activator.CreateInstance(objectType);

            JToken token = jObject["error"];
            if (token != null)
            {
                ((Response)instance).Error = (token.Value<string>());
                return instance;
            }

            if (instance is SimpleResponse response)
                InitializeSimpleResponse(response, jObject);
            return instance;
        }

        protected virtual object Parse(JObject jObject, Type objectType, JsonSerializer serializer)
        {
            return CreateInstance(jObject, objectType);
        }

        protected virtual object Parse(JArray jArray, Type objectType, JsonSerializer serializer)
        {
            var instance = Activator.CreateInstance(objectType);
            if (!jArray.HasValues)
                return instance;

            var tList = objectType.FindGenericType("List");
            if (tList == null)
                throw new ArgumentException($"objectType is expected to be List<T>");

            var tType = tList.GetGenericArguments().First();

            var addMethod = tList.GetMethod("Add", new[] {tType});
            if (addMethod == null)
                throw new Exception($"Add method was not found");

            foreach (JToken token in jArray)
            {
                if (token.Type == JTokenType.Object)
                {
                    var value = serializer.Deserialize(token.CreateReader(), tType);
                    addMethod.Invoke(instance, new[] {value});
                }
            }
            
            return instance;
        }
    }
}