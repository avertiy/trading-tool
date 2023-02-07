using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Engine.Emulator.Decisions
{
    public interface IDecision
    {
    }

    public class DoNothing : IDecision
    {
        public override string ToString()
        {
            return "";
        }
    }

    public class PostOrderDecision : IDecision
    {
        public OrderType Type { get; }
        public double Price { get; }
        public double Amount { get; }
        public string Pair { get; }

        public PostOrderDecision(OrderType type, double price, double amount, string pair)
        {
            Type = type;
            Price = price;
            Amount = amount;
            Pair = pair;
        }

        public override string ToString()
        {
            return $"{Type,4} {Price.FormatAsPrice()} x {Amount.FormatAsQuantity()}";
        }
    }

    public class MoveOrderDecision : IDecision
    {
    }

    public class CancelOrderDecision : IDecision
    {
        public string OrderId { get; }
        public string Pair { get; }

        public CancelOrderDecision(string orderId, string pair)
        {
            OrderId = orderId;
            Pair = pair;
        }

        public override string ToString()
        {
            return $"cancel order #{OrderId}";
        }
    }
}