using Autofac;
using AVS.CoreLib.DependencyRegistrar;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.Infrastructure.Config;
using AVS.PoloniexApi.LiveTools;

namespace AVS.Trading.OrderBookWatch.Infrastructure
{
    public class DependencyRegistrar: IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, IAppConfig config)
        {
            builder.RegisterType<PoloniexChannelClient>().AsSelf().SingleInstance();
            builder.RegisterType<WatchOrderBookService>().AsSelf().SingleInstance();
            
        }

        public int Order =>100;
    }
}
