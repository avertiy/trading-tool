using System;
using System.Configuration;
using System.Linq;
using Autofac;
using AVS.BinanceApi;
using AVS.CoreLib.Data.EF;
using AVS.CoreLib.Data.Utils;
using AVS.CoreLib.DependencyRegistrar;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.Infrastructure.Config;
using AVS.CoreLib.Services.Logging.LogWriters;
using AVS.ExmoApi;
using AVS.ExmoApi.Data.Domain;
using AVS.KunaApi;
using AVS.PoloniexApi;
using AVS.PoloniexApi.General;
using AVS.PoloniexApi.LiveTools;
using AVS.Trading.Core.Formatters;
using AVS.Trading.Framework.Adapters;
using AVS.Trading.Framework.Services;
using AVS.Trading.Framework.Services.LendingTools;
using AVS.Trading.Framework.Services.MarketTools;
using AVS.Trading.Framework.Services.TradingTools;
using AVS.Trading.Framework.Services.WalletTools;
using AVS.Trading.Framework.Utils;
using AVS.Trading.Core.Services;
using AVS.Trading.Data;
using AVS.Trading.Data.Services.MarketTools;
using AVS.Trading.Data.Services.System;
using AVS.Trading.Data.Services.TradingTools;

namespace AVS.Trading.Framework.Infrastructure
{
    public class DependencyRegistrar: DependencyRegistrarBase
    {
        protected override void RegisterServices(ContainerBuilder builder, ITypeFinder typeFinder, IAppConfig config)
        {
            if (config.NoDatabase)
            {
                builder.Register(c => new InMemoryDbContext()).AsSelf().As<IDbContext>().InstancePerLifetimeScope();
            }
            else
            {
                //ensure connection strings are not missing
                var connectionString = SqlConnectionHelper.GetConnectionString("PoloniexContext");
                var connectionString2 = SqlConnectionHelper.GetConnectionString("PoloniexContext2");

                builder.Register(c =>
                {
                    var ctx = new TradingDbContext(connectionString, connectionString2);
                    ctx.ConfigurationsLoader.AddAssembly(typeof(ExmoTradeItemMap).Assembly);
                    return ctx;
                }).AsSelf().As<IDbContext>().InstancePerLifetimeScope();
            }

            var formatProviderParameter = new TypedParameter(typeof(IFormatProvider), TradingFormatter.FormatProvider);

            builder.RegisterType<TaskLogWriter>().AsSelf().WithParameter(formatProviderParameter).InstancePerLifetimeScope();
            builder.RegisterType<MarketToolsService>().As<IMarketToolsService>().InstancePerLifetimeScope();
            builder.RegisterType<MarketDataPreprocessor>().As<IMarketDataPreprocessor>().InstancePerLifetimeScope();

            builder.RegisterType<TradingToolsService>().As<ITradingToolsService>().InstancePerLifetimeScope();
            builder.RegisterType<MarginToolsService>().As<IMarginToolsService>().InstancePerLifetimeScope();
            builder.RegisterType<TradingDataPreprocessor>().As<ITradingDataPreprocessor>().InstancePerLifetimeScope();
            builder.RegisterType<ImportDataService>().As<IImportDataService>().InstancePerLifetimeScope();

            builder.RegisterType<WalletToolsService>().As<IWalletToolsService>().InstancePerLifetimeScope();
            builder.RegisterType<LendingToolsService>().As<ILendingToolsService>().InstancePerLifetimeScope();
            builder.RegisterType<WalletDataPreprocessor>().As<IWalletDataPreprocessor>().InstancePerLifetimeScope();
            builder.RegisterType<LendingToolsDataPreprocessor>().As<ILendingToolsDataPreprocessor>().InstancePerLifetimeScope();

            builder.RegisterType<MarketToolsDataAdapter>().AsSelf().WithStaticCacheParameter().InstancePerLifetimeScope();
            builder.RegisterType<TradingToolsDataAdapter>().AsSelf().InstancePerLifetimeScope();
            builder.RegisterType<TradingToolsFacade>().AsSelf().InstancePerLifetimeScope();
            
            RegisterEntityServices(builder, typeFinder, (TradingAppConfig) config);
            RegisterOther(builder, typeFinder, (TradingAppConfig)config);
        }

        protected void RegisterOther(ContainerBuilder builder, ITypeFinder typeFinder, TradingAppConfig config)
        {
            builder.Register(c => config).As<TradingAppConfig>().As<AppConfig>().SingleInstance();

            var directory = new ExchangeDirectory();
            //config
            var poloniex = config.Exchanges["Poloniex"];
            if (poloniex != null)
            {
                var apiKey = poloniex.Keys.First();
                var client = new PoloniexClient(apiKey);
                client.Validate();
                builder.Register(c => client).As<PoloniexClient>().InstancePerLifetimeScope();

                builder.RegisterType<PoloniexChannelClient>().As<PoloniexChannelClient>().SingleInstance();
                directory.Register(client);
            }

            var exmo = config.Exchanges["Exmo"];
            if (exmo != null)
            {
                var apiKey = exmo.Keys.First();
                var exmoClient = new ExmoClient(apiKey);
                exmoClient.Validate();
                builder.Register(c => exmoClient).As<ExmoClient>().InstancePerLifetimeScope();
                directory.Register(exmoClient);
            }

            var kuna = config.Exchanges["Kuna"];
            if (kuna != null)
            {
                var apiKey = kuna.Keys.First();
                var kunaClient = new KunaClient(apiKey);
                kunaClient.Validate();
                builder.Register(c => kunaClient).As<KunaClient>().InstancePerLifetimeScope();
                directory.Register(kunaClient);
            }

            var binance = config.Exchanges["Binance"];
            if (binance != null)
            {
                var apiKey = binance.Keys.First();
                var binanceClient = new BinanceClient(apiKey);
                binanceClient.Validate();
                builder.Register(c => binanceClient).As<BinanceClient>().InstancePerLifetimeScope();
                directory.Register(binanceClient);
            }

            if (directory.GetAllClients().Length == 0)
                throw new ConfigurationErrorsException("At least one exchange and api key must be provided [check Exchanges node in app config]");
            
            builder.Register(c => directory).As<ExchangeDirectory>().SingleInstance();
            builder.RegisterType<ClearDataService>().As<IClearDataService>().InstancePerLifetimeScope();
            builder.RegisterType<DefaultLendingContextAnalizer>().As<ILendingContextAnalizer>().InstancePerDependency();
            builder.RegisterType<BalanceHelper>().As<IBalanceHelper>().InstancePerDependency();
        }

        protected void RegisterEntityServices(ContainerBuilder builder, ITypeFinder typeFinder, TradingAppConfig config)
        {
            //markets
            builder.RegisterType<MarketDataEntityService>().As<IMarketDataEntityService>().InstancePerLifetimeScope();
            builder.RegisterType<MarketTradeItemEntityService>().As<IMarketTradeItemEntityService>().InstancePerLifetimeScope();
            builder.RegisterType<ChartDataEntityService>().As<IChartDataEntityService>().InstancePerLifetimeScope();
            builder.RegisterType<OrderBookEntityService>().As<IOrderBookEntityService>().InstancePerLifetimeScope();
            builder.RegisterType<SellOrderEntityService>().As<ISellOrderEntityService>().InstancePerLifetimeScope();
            builder.RegisterType<BuyOrderEntityService>().As<IBuyOrderEntityService>().InstancePerLifetimeScope();

            //trading
            builder.RegisterType<TradeItemEntityService>().As<ITradeItemEntityService>().InstancePerLifetimeScope();
            builder.RegisterType<OpenOrderEntityService>().As<IOpenOrderEntityService>().InstancePerLifetimeScope();
            builder.RegisterType<TradeSessionEntityService>().As<ITradeSessionEntityService>().InstancePerLifetimeScope();

            //system
            builder.RegisterType<SyncRecordEntityService>().As<ISyncRecordEntityService>().InstancePerLifetimeScope();
        }

        public override int Order =>0;
    }
}
