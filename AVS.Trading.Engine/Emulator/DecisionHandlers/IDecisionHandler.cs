using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.Models;
using AVS.Trading.Core.Services;
using AVS.Trading.Data.Domain.TradingTools;
using AVS.Trading.Engine.Emulator.Context;
using AVS.Trading.Engine.Emulator.Decisions;

namespace AVS.Trading.Engine.Emulator.DecisionHandlers
{
    public interface IDecisionHandler
    {
        IResult Execute(IDecision decision, AlgorithmContext ctx);
    }

    public class DecisionHandler : IDecisionHandler
    {
        private readonly IDecisionHandlerService _service;

        public DecisionHandler(IDecisionHandlerService service)
        {
            _service = service;
        }

        public IResult Execute(IDecision decision, AlgorithmContext ctx)
        {
            IResult result;
            switch (decision)
            {
                case PostOrderDecision p:
                    var postOrderResult =  _service.PostOrder(p.Pair, p.Type, p.Price, p.Amount);
                    if (postOrderResult.Success)
                    {
                        ctx.State.OpenOrderIds.Add(postOrderResult.OrderId);
                        //ctx.State.Balance
                    }

                    result = postOrderResult;
                    break;
                case MoveOrderDecision m:
                    result = new EmptyResult();
                    break;
                case CancelOrderDecision c:
                    var cancleOrderResult = _service.CancelOrder(c.Pair, c.OrderId);
                    if (cancleOrderResult.Success)
                        ctx.State.OpenOrderIds.Remove(cancleOrderResult.OrderId);
                    result = cancleOrderResult;
                    break;
                default:
                    result = new EmptyResult();
                    break;
            }

            result.Balance = ctx.State.Balance;
            result.Decision = decision;
            return result;
        }
    }

   
}