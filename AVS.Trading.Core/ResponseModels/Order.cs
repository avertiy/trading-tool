using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.MarketTools;

namespace AVS.Trading.Core.ResponseModels
{
    /// <summary>
    /// order book order
    /// </summary>
    public class Order : IOrder
    {
        public Order()
        {
        }

        public Order(double price, double amountQuote)
        {
            Price = price;
            AmountQuote = amountQuote;
        }
        
        public double Price { get; private set; }

        public double AmountQuote { get; set; }

        public double AmountBase => (AmountQuote * Price).Normalize();

        public override string ToString()
        {
            return $"{Price.FormatAsPrice()}x{AmountQuote.FormatNumber()}={AmountBase.FormatNumber()}";
        }
    }
}
