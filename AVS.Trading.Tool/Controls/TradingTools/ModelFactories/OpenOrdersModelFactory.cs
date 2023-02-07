using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AVS.CoreLib._System.Debug;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Enums;
using AVS.Trading.Tool.Controls.Common;
using AVS.Trading.Tool.Controls.Extensions;
using AVS.Trading.Framework.Adapters;
using AVS.Trading.Data.Domain.TradingTools;
using AVS.Trading.Data.Services.TradingTools;
using AVS.Trading.Framework.Infrastructure;

namespace AVS.Trading.Tool.Controls.TradingTools.ModelFactories
{
    public class OpenOrdersModelFactory
    {
        private readonly TradingToolsDataAdapter _dataAdapter;
        private readonly TradingToolsFacade _facade;

        public OpenOrdersModelFactory(TradingToolsDataAdapter dataAdapter, TradingToolsFacade facade)
        {
            _dataAdapter = dataAdapter;
            _facade = facade;
        }

        public async Task<Response<IList<OpenOrder>>> LoadOrdersAsync(ITradeHistoryFilters filters)
        {
            var response = await _facade.GetOpenOrdersAsync(filters.Market);
            return response.OnSuccess(() => filters.ApplyFilters(response.Data));
        }


    }
}