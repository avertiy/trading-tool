using AVS.Trading.Core.Models;
using AVS.Trading.Engine.Emulator.Decisions;

namespace AVS.Trading.Engine.Emulator.DecisionHandlers
{
    public interface IResult
    {
        IDecision Decision { get; set; }
        BalanceSheet Balance { get; set; }
    }

    public abstract class Result : IResult
    {
        public IDecision Decision { get; set; }
        public BalanceSheet Balance { get; set; }
    }

    public class EmptyResult : Result
    {
        public override string ToString()
        {
            return "empty";
        }
    }

    public class PostOrderResult : Result
    {
        public bool Success { get; set; }
        public string OrderId { get; set; }

        public override string ToString()
        {
            if(Success)
                return $"{Decision}";
            return $"Post order failed";
        }
    }

    public class CancelOrderResult : Result
    {
        public bool Success { get; set; }
        public string OrderId { get; set; }

        public override string ToString()
        {
            if (Success)
                return $"canceled order {OrderId}";

            return "cancel order failed";
        }
    }
}