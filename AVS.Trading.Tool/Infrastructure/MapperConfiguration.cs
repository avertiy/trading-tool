using System;
using AutoMapper;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Tool.Models.Market;
using AVS.Trading.Tool.Models.Trading;
using AVS.Trading.Data.Domain.MarketTools.TradeHistory;
using AVS.Trading.Data.Domain.TradingTools;

namespace AVS.Trading.Tool.Infrastructure
{
    public class MapperConfiguration : IMapperConfiguration
    {
        public Action<IMapperConfigurationExpression> GetConfiguration()
        {
            Action<IMapperConfigurationExpression> action = cfg =>
            {
                cfg.CreateMap<TradeItem, TradeItemModel>().IgnoreAllNonExisting();
                cfg.CreateMap<MarketTradeItem, MarketTradeItemModel>().IgnoreAllNonExisting();
            };
            return action;
        }

        public int Order
        {
            get { return 1; }
        }
    }
}