using AVS.Trading.Core.ResponseModels;
using Newtonsoft.Json;

namespace AVS.ExmoApi.Models
{
    public class ExmoSimpleResponse : SimpleResponse
    {
        [JsonProperty("result")]
        public bool Result { get; internal set; }
        public override bool Success => Result && string.IsNullOrEmpty(Error);

        public void SetMessage(string message)
        {
            Message = message;
        }
    }
}