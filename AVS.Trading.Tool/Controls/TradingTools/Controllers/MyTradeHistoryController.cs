using AVS.CoreLib.WinForms.MVC;
using AVS.Trading.Tool.Controls.Common;
using AVS.Trading.Tool.Controls.TradingTools.ModelFactories;
using AVS.Trading.Tool.Models.Trading;
using AVS.Trading.Tool.Utils;

namespace AVS.Trading.Tool.Controls.TradingTools.Controllers
{
    public class MyTradeHistoryController : GridViewController<IMyTradeHistoryView, TradeItemModel>
    {
        private readonly TradeHistoryModelFactory _modelFactory;
        
        public MyTradeHistoryController(TradeHistoryModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
        }

        public override object LoadData(object argument)
        {
            var data = _modelFactory.GetTradeHistoryModel((ITradeHistoryFilters)argument, View.GridControl.ReportProgress);
            return data;
        }
        
        public override void GridSelectionChanged()
        {
            View.GridControl.GridSummaryText = ((MyGridSelectionHelper)GridSelectionHelper).GetTradeSummary(DataGrid);
        }

        protected override void InitializeGridSelectionHelper()
        {
            GridSelectionHelper = new MyGridSelectionHelper();
            GridSelectionHelper.Initialize(DataGrid.Columns);
        }
    }
}
