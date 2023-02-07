using Autofac;
using AVS.CoreLib.DependencyRegistrar;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.Infrastructure.Config;
using AVS.Trading.Data.Services.TradingTools;
using AVS.Trading.Engine.Emulator;
using AVS.Trading.Engine.Emulator.Algorithms;
using AVS.Trading.Engine.Emulator.Context;
using AVS.Trading.Engine.Emulator.DecisionHandlers;
using AVS.Trading.Engine.Emulator.DecisionStrategies;
using AVS.Trading.Engine.Emulator.Services;

namespace AVS.Trading.Engine.Infrastructure
{
    public class DependencyRegistrar: IDependencyRegistrar
    {
        public int Order =>50;

        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, IAppConfig config)
        {
            builder.RegisterType<AlgorithmFactory>().As<IAlgorithmFactory>().InstancePerLifetimeScope();
            builder.RegisterType<TradeSessionService>().As<ITradeSessionService>().InstancePerLifetimeScope();
            
            builder.RegisterType<DecisionHandler>().As<IDecisionHandler>().InstancePerLifetimeScope();
            builder.RegisterType<TradingEngine>().As<ITradingEngine>().AsSelf().InstancePerLifetimeScope();
            

            //emulator services
            builder.RegisterType<EmulatorDataProvider>().AsSelf().SingleInstance();
            builder.RegisterType<EmulatorContextBuilder>().As<IContextBuilder>().InstancePerLifetimeScope();
            builder.RegisterType<EmulatorDecisionHandlerService>().As<IDecisionHandlerService>().InstancePerLifetimeScope();
            builder.RegisterType<BalanceSheetHelper>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterGeneric(typeof(TradingEmulator<>)).AsSelf().InstancePerLifetimeScope();
            

            //algorithms
            builder.RegisterType<PriceService>().As<IPriceService>().InstancePerLifetimeScope();
            builder.RegisterType<SimpleDecisionStrategy>().AsSelf().As<IDecisionStrategy>().InstancePerLifetimeScope();
            builder.RegisterType<SimpleAlgorithm>().AsSelf().InstancePerLifetimeScope();
        }

    }
}
