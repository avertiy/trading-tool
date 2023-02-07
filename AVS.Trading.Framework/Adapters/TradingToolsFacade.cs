using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Services;
using AVS.Trading.Data.Domain.TradingTools;
using AVS.Trading.Data.Services.TradingTools;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Services.TradingTools;

namespace AVS.Trading.Framework.Adapters
{
    //Framework module exposes via Façade a higher-level interface
    //that makes easier to use GetXXX trading data 
    //Façade makes complex subsystem to query an exchange, convert DTO to domain entity models and extend data with extra properties 
    public class TradingToolsFacade
    {
        private readonly ITradingToolsService _tradingToolsService;
        private readonly IOpenOrderEntityService _openOrderEntityService;
        private readonly IWorkContext _workContext;
        private readonly ITradingDataPreprocessor _preprocessor;
        public TradingToolsFacade(ITradingToolsService tradingToolsService, IOpenOrderEntityService openOrderEntityService, IWorkContext workContext, ITradingDataPreprocessor preprocessor)
        {
            _tradingToolsService = tradingToolsService;
            _openOrderEntityService = openOrderEntityService;
            _workContext = workContext;
            _preprocessor = preprocessor;
        }

        public async Task<Response<IList<OpenOrder>>> GetOpenOrdersAsync(PairString pair)
        {
            //var cp = CurrencyPair.Parse(market);

            var response = await _tradingToolsService.GetOpenOrdersAsync(pair);
            var dbOrders = await _openOrderEntityService.GetAllAsync(o => o.Pair == pair.Value && o.State <= OrderState.Processing);
            return response.OnSuccess(() =>
            {
                return _preprocessor.PreprocessOrders(response.Data,
                    order =>
                    {
                        order.Pair = pair.Value;
                        var entity = dbOrders.FirstOrDefault(o => o.OrderNumber == order.OrderNumber);
                        //extend exchange data with custom properties data
                        if (entity != null)
                        {
                            order.CreatedOnUtc = entity.CreatedOnUtc;
                            order.TakeProfit = entity.TakeProfit;
                            order.StopLoss = entity.StopLoss;
                            order.State = entity.State;
                        }
                    }
                );

            });
        }
    }
}