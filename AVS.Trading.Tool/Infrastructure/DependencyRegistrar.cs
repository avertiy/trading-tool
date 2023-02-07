using Autofac;
using AVS.CoreLib.DependencyRegistrar;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.Infrastructure.Config;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Tool.Controls.MarketTools;
using AVS.Trading.Tool.Controls.MarketTools.Controllers;
using AVS.Trading.Tool.Controls.MarketTools.ModelFactories;
using AVS.Trading.Tool.Controls.TradingTools.ChildControls;
using AVS.Trading.Tool.Controls.TradingTools.Controllers;
using AVS.Trading.Tool.Controls.TradingTools.ModelFactories;
using AVS.Trading.Tool.Controls.WalletTools;

namespace AVS.Trading.Tool.Infrastructure
{
    public class DependencyRegistrar: IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder, ITypeFinder typeFinder, IAppConfig config)
        {
            builder.RegisterType<WorkContext>().As<IWorkContext>().SingleInstance();

            //controllers
            builder.RegisterType<OrderBookController>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<MarketTickerController>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<MarketTradeHistoryController>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<LoadMarketDataController>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<MyTradeHistoryController>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<MyOpenOrdersController>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<MyTradeBalancesController>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<MyLoansController>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<PostOrdersController>().AsSelf().InstancePerLifetimeScope();


            //model factories
            builder.RegisterType<MarketTradeHistoryModelFactory>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<TradeHistoryModelFactory>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<OpenOrdersModelFactory>().AsSelf().InstancePerLifetimeScope();
        }

        public int Order =>100;
    }
}
