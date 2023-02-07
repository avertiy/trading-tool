using AVS.CoreLib._System.Net;
using Newtonsoft.Json;

namespace AVS.Trading.Core.ResponseModels
{
    public class SimpleResponse : Response
    {
        [JsonProperty("success")]
        public int SuccessIntValue { get; internal set; }

        public override bool Success => SuccessIntValue == 1 && string.IsNullOrEmpty(Error);

        [JsonProperty("message")]
        public string Message { get; protected internal set; }

        public static implicit operator bool(SimpleResponse response)
        {
            return response != null && response.Success;
        }
    }
}