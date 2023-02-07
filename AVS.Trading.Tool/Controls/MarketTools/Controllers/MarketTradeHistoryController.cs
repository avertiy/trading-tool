using AVS.CoreLib.WinForms.MVC;
using AVS.Trading.Tool.Controls.Common;
using AVS.Trading.Tool.Controls.MarketTools.ModelFactories;
using AVS.Trading.Tool.Models.Market;
using AVS.Trading.Tool.Utils;

namespace AVS.Trading.Tool.Controls.MarketTools.Controllers
{
    public class MarketTradeHistoryController: GridViewController<IMarketTradeHistoryView, MarketTradeItemModel>
    {
        private readonly MarketTradeHistoryModelFactory _modelFactory;

        public MarketTradeHistoryController(MarketTradeHistoryModelFactory modelFactory)
        {
            _modelFactory = modelFactory;
        }


        public override object LoadData(object argument)
        {
            var model = _modelFactory.GetTradeHistoryModel((ITradeHistoryFilters) argument, View.GridControl.ReportProgress);
            return model;
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