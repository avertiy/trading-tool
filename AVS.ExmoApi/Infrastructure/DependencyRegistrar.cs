using Autofac;
using AVS.CoreLib.DependencyRegistrar;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.Infrastructure.Config;
using AVS.ExmoApi.Data.Services;
using AVS.ExmoApi.Services;
using AVS.ExmoApi.Tasks;
using AVS.ExmoApi.TradingTools;

namespace AVS.ExmoApi.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, IAppConfig config)
        {
            builder.RegisterType<ExmoSymbolService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ExmoTradingDataPreprocessor>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ExmoTradeItemEntityService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ExmoWalletEntityService>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ExmoWalletSubtask>().AsSelf().InstancePerLifetimeScope();

            //builder.RegisterType<ExmoLoadTradesSubtask>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<ExmoPairProvider>().AsSelf().SingleInstance();
        }

        public int Order => 10;
    }
}
