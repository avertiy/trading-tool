using System;
using AutoMapper;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Core.Interfaces.LendingTools;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Core.Interfaces.WalletTools;
using AVS.Trading.Core.ResponseModels;
using AVS.Trading.Data.Domain.Market;
using AVS.Trading.Data.Domain.MarketTools;
using AVS.Trading.Data.Domain.MarketTools.TradeHistory;
using AVS.Trading.Data.Domain.TradingTools;
using AVS.Trading.Data.Domain.WalletTools;
using MarketData = AVS.Trading.Data.Domain.MarketTools.MarketData;

namespace AVS.Trading.Framework.Infrastructure
{
    public class MapperConfiguration : IMapperConfiguration
    {
        public Action<IMapperConfigurationExpression> GetConfiguration()
        {
            Action<IMapperConfigurationExpression> action = cfg =>
            {
                cfg.CreateMap<IMarketData, MarketData>().IgnoreAllNonExisting();
                cfg.CreateMap<ICandlestick, ChartDataItem>().IgnoreAllNonExisting();
                cfg.CreateMap<AVS.Trading.Core.Interfaces.MarketTools.IMarketTrade, MarketTradeItem>().IgnoreAllNonExisting();
                
                cfg.CreateMap<Order, BuyOrder>().IgnoreAllNonExisting();
                cfg.CreateMap<Order, SellOrder>().IgnoreAllNonExisting();

                cfg.CreateMap<AVS.Trading.Core.Interfaces.TradingTools.ITrade,
                        TradeItem>()
                    .ForMember(dest => dest.OrderId, src => src.MapFrom(t => t.OrderNumber))
                    .ForMember(dest => dest.TradeId, src => src.MapFrom(t => t.IdTrade));

                cfg.CreateMap<IActiveLoan,ActiveLoan>().IgnoreAllNonExisting();

            };
            return action;
        }

        public int Order
        {
            get { return 1; }
        }
    }
}