using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVS.CoreLib.WinForms.MVC;
using AVS.Trading.Framework.Services.MarketTools;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Models;
using AVS.Trading.Core.ResponseModels.MarketTools;

namespace AVS.Trading.Tool.Controls.MarketTools
{
    public interface IChartDataView
    {
        //string Market { get; }
        //MarketPeriod Period { get; }
        //DateRange DateRange { get; }
    }

    public partial class ChartDataControl : MyUserControl, IChartDataView
    {
        private ChartDataController _controller;
        protected ChartDataController Controller => _controller ?? (_controller = GetController<ChartDataController>());

        protected override void Initialize()
        {
            InitializeComponent();
            this.StatusLabel = toolStripStatusLabel1;
        }

        public void Setup(string market)
        {
            selectMarketControl1.Market = market;
            selectDateRangeControl1.Range = new DateRange() {From = DateTime.Today.AddDays(-180), To = DateTime.Now};
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            _controller.LoadChartData(Market, Period, DateRange);
        }

        public string Market => selectMarketControl1.Market;
        public MarketPeriod Period => selectMarketPeriodControl1.SelectedPeriod;
        public DateRange DateRange => selectDateRangeControl1.Range;
    }

    public class ChartDataController : ControllerBase<IChartDataView>
    {
        private readonly IMarketToolsService _marketToolsService;
        private readonly IMarketDataPreprocessor _dataPreprocessor;

        public ChartDataController(IMarketToolsService marketToolsService, IMarketDataPreprocessor dataPreprocessor)
        {
            _marketToolsService = marketToolsService;
            _dataPreprocessor = dataPreprocessor;
        }

        public async void LoadChartData(string market, MarketPeriod period, DateRange dateRange)
        {
            var response = await _marketToolsService.LoadChartDataAsync(market, period, dateRange);
            if (response.Success)
            {
                _dataPreprocessor.PreprocessChartData(response.Data, market, period, dateRange.From, dateRange.To);
            }
        }
    }
}
