using Autofac;
using AVS.CoreLib.DependencyRegistrar;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.Infrastructure.Config;
using AVS.BinanceApi.MarketTools;
using AVS.BinanceApi.Services;
using AVS.BinanceApi.TradingTools;

namespace AVS.BinanceApi.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, IAppConfig config)
        {
            builder.RegisterType<BinanceTradingDataPreprocessor>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BinanceMarketToolsPreprocessor>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BinanceSymbolService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<BinancePairProvider>().AsSelf().SingleInstance();
            builder.RegisterType<BinancePairUsageService>().AsSelf().SingleInstance();
            builder.RegisterType<BinanceBackgroundTask>().As<IBackgroundTask>().SingleInstance();
        }

        public int Order => 10;
    }
}
