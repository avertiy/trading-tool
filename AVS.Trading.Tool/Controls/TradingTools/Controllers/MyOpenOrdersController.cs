using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVS.CoreLib._System.Net;
using AVS.CoreLib.WinForms.Grid;
using AVS.CoreLib.WinForms.MVC;
using AVS.Trading.Tool.Controls.Common;
using AVS.Trading.Tool.Controls.TradingTools.ModelFactories;
using AVS.Trading.Core;
using AVS.Trading.Core.Services;
using AVS.Trading.Data.Domain.TradingTools;

namespace AVS.Trading.Tool.Controls.TradingTools.Controllers
{
    public class MyOpenOrdersController : GridViewController<IMyOpenOrdersView, OpenOrder>
    {
        private readonly OpenOrdersModelFactory _modelFactory;
        private readonly ITradingToolsService _tradingToolsService;
        

        public MyOpenOrdersController(ITradingToolsService tradingToolsService, OpenOrdersModelFactory modelFactory)
        {
            _tradingToolsService = tradingToolsService;
            _modelFactory = modelFactory;
        }
        
        public override object LoadData(object argument)
        {
            throw new NotSupportedException("Use LoadDataAsync instead");
        }

        public Task<Response<IList<OpenOrder>>> LoadDataAsync(ITradeHistoryFilters filters)
        {
            return _modelFactory.LoadOrdersAsync(filters);
        }

        public void CancelOrders(GridControl gridControl)
        {
            var grid = gridControl.DataGrid;
            var ordersToBeCanceled = new Dictionary<string, string>();//<ordernumber,market>
            if (grid.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in grid.SelectedRows)
                {
                    var order = (OpenOrder) row.DataBoundItem;
                    ordersToBeCanceled.Add(order.OrderNumber, order.Pair);
                }
            }
            else if (grid.SelectedCells.Count > 0)
            {
                foreach (DataGridViewCell cell in grid.SelectedCells)
                {
                    var order = (OpenOrder)cell.OwningRow.DataBoundItem;
                    if(!ordersToBeCanceled.ContainsKey(order.OrderNumber))
                        ordersToBeCanceled.Add(order.OrderNumber,order.Pair);
                }
            }
            
            if (ordersToBeCanceled.Count > 0)
            {
                foreach (KeyValuePair<string, string> kp in ordersToBeCanceled)
                {
                    _tradingToolsService.CancelOrder(kp.Value, kp.Key);
                }
            }
                
        }

        public void GridSelectionChanged(DataGridView grid)
        {
            if (grid.SelectedCells.Count == 0)
                return;
            var column = grid.SelectedCells[0].OwningColumn;
            if ((column.Name != "Amount" && column.Name != "Total"))
                return;

            var summary = new OrderSummary();
            foreach (DataGridViewCell cell in grid.SelectedCells)
            {
                //ignore other selected columns
                if (cell.ColumnIndex != column.Index)
                    continue;
                if (cell.Value is string || !cell.ValueType.IsValueType)
                    continue;

                var order = grid.Rows[cell.RowIndex].DataBoundItem as OpenOrder;
                if(order == null)
                    continue;
                summary.Market = order.Pair;
                summary.Add(order.Account, order.Type, order.AmountQuote, order.AmountBase);
            }
        }
    }
}