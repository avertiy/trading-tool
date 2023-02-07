using System;
using System.Collections.Generic;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Interfaces.LendingTools;
using AVS.Trading.Core.Interfaces.TradingTools;
using Newtonsoft.Json;

namespace AVS.Trading.Core.ResponseModels.TradingTools
{
    public class TradeHistory : Response<IEnumerable<ITrade>>
    {

    }

    public interface ILoanOrders
    {
        string Currency { get; set; }
        IList<LoanOrderItem> Offers { get; set; }
        IList<LoanOrderItem> Demands { get; set; }
    }

    public class LoanOrders: ILoanOrders
    {
        public LoanOrders()
        {
            Offers = new List<LoanOrderItem>();
            Demands = new List<LoanOrderItem>();
        }

        [JsonProperty("offers")]
        public IList<LoanOrderItem> Offers { get; set; }

        [JsonProperty("demands")]
        public IList<LoanOrderItem> Demands { get; set; }

        public string Currency { get; set; }
    }

    public class LoanOrderItem
    {
        [JsonProperty("rate")]
        public double Rate { get; set; }
        [JsonProperty("rate")]
        public double Amount { get; set; }
        [JsonProperty("rangeMin")]
        public double RangeMin { get; set; }
        [JsonProperty("rangeMax")]
        public double RangeMax { get; set; }
    }

    public class CreateLoanOfferResponse : Response, IOpenLoanOffer
    {
        public long Id { get; set; }
        public double Rate { get; set; }
        public double Amount { get; set; }
        public int Duration { get; set; }
        public DateTime DateUtc { get; set; }
        public string Currency { get; set; }
        public string Exchange { get; set; }
        public bool AutoRenew { get; set; }
    }
}