namespace AVS.Trading.Core.Interfaces
{
    public class BalanceInfo
    {
        public string QuoteCurrency { get; set; }
        public double QuoteAmount { get; set; }
        public double QuoteAmountOnOrders { get; set; }
        public double QuoteTradableAmount { get; set; }
        public double QuoteAmountOnMarginAccount { get; set; }

        public string BaseCurrency { get; set; }
        public double BaseAmount { get; set; }
        public double BaseAmountOnOrders { get; set; }
        public double BaseTradableAmount { get; set; }
        public double BaseAmountOnMarginAccount { get; set; }
    }
}