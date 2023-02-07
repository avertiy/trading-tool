using System;
using System.Collections.Generic;
using AVS.Trading.Core;
using AVS.Trading.Data.Domain.TradingTools;
using AVS.Trading.Engine.Emulator.Algorithms;
using AVS.Trading.Engine.Emulator.Context;
using AVS.Trading.Engine.Emulator.DecisionHandlers;
using AVS.Trading.Engine.Emulator.Decisions;
using AVS.Trading.Engine.Emulator.Services;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Pipeline.Models;
using AVS.Trading.Pipeline.TradingAlgorithms.Context;

namespace AVS.Trading.Engine.Emulator
{
    public interface ITradingEngine
    {
        IResult Run(Parameters parameters);
    }

    public class TradingEngine : ITradingEngine
    {
        public IAlgorithmFactory Factory;
        private readonly ITradeSessionService _tradeSessionService;
        private readonly IContextBuilder _contextBuilder;
        private readonly IDecisionHandler _decisionHandler;
        private readonly IWorkContext _workContext;

        public TradingEngine(IAlgorithmFactory factory, 
            ITradeSessionService tradeSessionService, 
            IContextBuilder contextBuilder, 
            IDecisionHandler decisionHandler, 
            IWorkContext workContext)
        {
            Factory = factory;
            _tradeSessionService = tradeSessionService;
            _contextBuilder = contextBuilder;
            _decisionHandler = decisionHandler;
            _workContext = workContext;
        }

        public IResult Run(Parameters parameters)
        {
            _workContext.Exchange = parameters.Exchange;
            TradeSession session = _tradeSessionService.GetOpenSession(parameters.Exchange, parameters.Pair);
            if (session == null)
            {
                session = _tradeSessionService.OpenSession(parameters.Exchange, parameters.Pair, parameters.Algorithm);
                session.Amount = parameters.Amount;
                session.BalanceSheet = parameters.InitialBalance;
                _tradeSessionService.SaveSession(session);
            }
            if(session.Algorithm != parameters.Algorithm)
                 throw new ArgumentException($"Open trade session {session} already exists [algorithm mismatch {parameters.Algorithm}]");
            
            IAlgorithm alg = Factory.GetAlgorithm(session.Algorithm);
            if (alg == null)
                throw new NullReferenceException("algorithm is required");
            
            AlgorithmContext ctx = _contextBuilder.Build(session, alg.ContextSetup);
            ctx.RestoreState(session);
            IDecision decision;
            try
            {
                decision = alg.Execute(ctx);
            }
            catch (Exception ex)
            {
                throw new Exception($"{session.Algorithm} failed", ex);
            }
            
            if (decision == null)
                throw new NullReferenceException("decision is required");

            IResult result = _decisionHandler.Execute(decision, ctx);
            if (result == null)
                throw new NullReferenceException("result is required");

            //at this point an order is posted but not executed by exchange
            ctx.SaveState(session);
            _tradeSessionService.SaveSession(session);

            return result;
        }
    }
}