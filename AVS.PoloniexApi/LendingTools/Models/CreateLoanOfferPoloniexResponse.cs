using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVS.Trading.Core.ResponseModels;
using Newtonsoft.Json;

namespace AVS.PoloniexApi.LendingTools.Models
{
    internal class CreateLoanOfferPoloniexResponse: SimpleResponse
    {
        [JsonProperty("orderID")]
        public long OfferNumber { get; set; }
    }
}
