using AVS.Trading.Core.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System;

namespace AVS.ExmoApi.TradingTools.Models
{
    //    {
    //    "result": true,
    //    "error": "",
    //    "begin": "1493942400",
    //    "end": "1494028800",
    //    "history": [{
    //        "dt": 1461841192,
    //        "type": "deposit",
    //        "curr": "RUB",
    //        "status": "processing",
    //        "provider": "Qiwi (LA) [12345]",
    //        "amount": "1",
    //        "account": "",
    //    },
    //    {
    //    "dt": 1463414785,
    //    "type": "withdrawal",
    //    "curr": "USD",
    //    "status": "paid",
    //    "provider": "EXCODE",
    //    "amount": "-1",
    //    "account": "EX-CODE_19371_USDda...",
    //}
    //]
    //}

    public class WalletHistory
    {
        [JsonProperty("result")]
        public bool Result { get; set; }

        [JsonIgnore]
        public bool Success => Result && string.IsNullOrEmpty(Error);

        [JsonProperty("error")]
        public string Error { get; set; }
        [JsonProperty("begin")]
        private string Begin
        {
            set
            {
                From = ulong.Parse(value).UnixTimeStampToDateTime();
            }
        }
        [JsonIgnore]
        public DateTime From { get; set; }
        [JsonProperty("end")]
        private string End
        {
            set
            {
                To = ulong.Parse(value).UnixTimeStampToDateTime();
            }
        }
        [JsonIgnore]
        public DateTime To { get; set; }
        [JsonProperty("history")]
        public List<WalletHistoryItem> Items { get; set; }
    }
    

    public class WalletHistoryItem
    {
        [JsonProperty("dt")]
        private string TimeInternal
        {
            set
            {
                DateUtc = ulong.Parse(value).UnixTimeStampToDateTime();
            }
        }
        [JsonIgnore]
        public DateTime DateUtc { get; private set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("curr")]
        public string Currency { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; }
        [JsonProperty("provider")]
        public string Provider { get; set; }
        [JsonProperty("account")]
        public string Account { get; set; }
        [JsonProperty("amount")]
        public double Amount { get; set; }
    }
}