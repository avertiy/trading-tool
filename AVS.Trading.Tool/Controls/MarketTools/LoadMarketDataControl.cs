using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.WinForms.MVC;
using AVS.Trading.Framework.Services.MarketTools;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Models;

namespace AVS.Trading.Tool.Controls.MarketTools
{
    public partial class LoadMarketDataControl : UserControl
    {
        protected LoadMarketDataController Controller => EngineContext.Current
            .Resolve<LoadMarketDataController>();
        public LoadMarketDataControl()
        {
            InitializeComponent();
        }

        private async void btnLoadChartData_Click(object sender, EventArgs e)
        {
            await Controller.LoadChartDataAsync(txtCurrencyPair.Text);
        }

        private async void btnLoadMarketSummary_Click(object sender, EventArgs e)
        {
            await Controller.LoadMarketSummaryAsync();
        }
    }

    public class LoadMarketDataController : ControllerBase
    {
        private readonly IMarketToolsService _loadMarketService;
        private readonly IMarketDataPreprocessor _dataPreprocessor;
        private readonly IImportDataService _importDataService;

        public LoadMarketDataController(IMarketToolsService loadMarketService, IMarketDataPreprocessor dataPreprocessor,
            IImportDataService importDataService)
        {
            _loadMarketService = loadMarketService;
            _dataPreprocessor = dataPreprocessor;
            _importDataService = importDataService;
        }

        public async Task LoadTradeHistory(string market, DateTime from, DateTime to)
        {
            await SafeExecute(async () =>
            {
                //load 1 month history
                var hours = -4;//1 day for testing
                var data = await _loadMarketService.LoadTradeHystoryAsync(market, DateTime.UtcNow.AddHours(hours), DateTime.UtcNow);
            });
        }

        public async Task LoadMarketSummaryAsync()
        {
            await SafeExecute(async () =>
            {
                await _loadMarketService.GetTickerAsync();
            });
        }

        public async Task LoadChartDataAsync(string market)
        {
            await SafeExecute(async () =>
            {
                await _loadMarketService.LoadChartDataAsync(market, MarketPeriod.D, new DateRange(DateTime.Today.Date.ToUniversalTime(), DateTime.UtcNow));
            });
        }

    }
}
