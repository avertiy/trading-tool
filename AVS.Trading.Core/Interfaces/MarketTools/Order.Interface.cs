namespace AVS.Trading.Core.Interfaces.MarketTools
{
    public interface IOrder
    {
        double Price { get; }
        double AmountQuote { get; }
        double AmountBase { get; }
    }
}
