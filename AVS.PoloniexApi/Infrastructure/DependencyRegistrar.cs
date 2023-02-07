using Autofac;
using AVS.CoreLib.DependencyRegistrar;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.Infrastructure.Config;
using AVS.PoloniexApi.General;

namespace AVS.PoloniexApi.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, IAppConfig config)
        {
            
            //builder.RegisterType<PoloniexDataPreprocessor>().AsSelf().InstancePerLifetimeScope();
            //builder.RegisterType<PoloniexTradeItemEntityService>().AsSelf().InstancePerLifetimeScope();
            //builder.RegisterType<PoloniexWalletEntityService>().AsSelf().InstancePerLifetimeScope();
            
            builder.RegisterType<PoloniexPairProvider>().AsSelf().SingleInstance();
        }

        public int Order => 10;
    }
}
