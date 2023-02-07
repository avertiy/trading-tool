using Autofac;
using AVS.CoreLib.DependencyRegistrar;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.Infrastructure.Config;
using AVS.KunaApi.MarketTools;
using AVS.KunaApi.Services;
using AVS.KunaApi.TradingTools;

namespace AVS.KunaApi.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, IAppConfig config)
        {
            
            builder.RegisterType<KunaTradingDataPreprocessor>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<KunaMarketToolsPreprocessor>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<KunaSymbolService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<KunaPairProvider>().AsSelf().SingleInstance();
        }

        public int Order => 10;
    }
}
