using System;
using System.Linq;
using System.Threading.Tasks;
using AVS.BinanceApi.Services;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Core.Interfaces.TradingTools;

namespace AVS.BinanceApi.Infrastructure
{
    public class BinanceBackgroundTask : IBackgroundTask
    {
        private readonly BinanceClient _client;
        private readonly BinancePairUsageService _pairUsageService;
        public BinanceBackgroundTask(BinanceClient client, BinancePairUsageService pairUsageService)
        {
            _client = client;
            _pairUsageService = pairUsageService;
        }

        public async Task ExecuteAsync()
        {
            var pairs = _client.Pairs.GetAllPairs().ToArray();
            var request = new GetTradesRequest()
            {
                Limit = 10000,
                Pairs = pairs
            };

            _pairUsageService.Init(pairs);

            // get all recent trades and setup pair usage
            var response = await _client.Trading.GetAllTradesAsync(request);
            if (response.Success)
            {
                var date = _pairUsageService.DueDate;
                foreach (var kp in response.Data)
                {
                    var count = kp.Value.Count(x => x.DateUtc >= date);
                    _pairUsageService.Update(kp.Key, count);
                }
            }
        }

        public int Order => 10;
    }
}