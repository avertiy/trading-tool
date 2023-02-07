using System;
using System.Linq;
using AVS.CoreLib.Services.Logging.LogWriters;
using AVS.ExmoApi.Data.Services;
using AVS.ExmoApi.TradingTools;

namespace AVS.ExmoApi.Tasks
{
    public class ExmoWalletSubtask
    {
        private readonly ExmoClient _client;
        private readonly ExmoWalletEntityService _walletEntityService;
        private readonly ExmoTradingDataPreprocessor _dataPreprocessor;

        public ExmoWalletSubtask(ExmoTradingDataPreprocessor dataPreprocessor, ExmoClient client,
            ExmoWalletEntityService walletEntityService)
        {
            this._dataPreprocessor = dataPreprocessor;
            this._client = client;
            this._walletEntityService = walletEntityService;
        }

        static DateTime _lastCheckedDate = new DateTime(2018, 2, 28);

        public void LoadWalletHistory(TaskLogWriter log)
        {
            log.Write($"Executing ExmoWalletSubtask");
            var last = _walletEntityService.GetLast();
            if (last != null && last.DateUtc > _lastCheckedDate)
            {
                if(last.DateUtc.Date < DateTime.Today)
                    _lastCheckedDate = last.DateUtc.Date.AddDays(1);
                else
                    return;
            }

            if(_lastCheckedDate >= DateTime.Today)
                return;

            var start = _lastCheckedDate;

            for (int i = 0; i < 4; i++)
            {
                var data = _client.WalletTools.GetWalletHistory(start.AddDays(i));
                if(data.Error!=null && data.Error.StartsWith("API rate limit exceeded"))
                {
                    return;
                }

                var transactions = _dataPreprocessor.PreprocessWalletHistory(data);

                if (transactions.Length > 0)
                {
                    _walletEntityService.BulkInsert(transactions);
                    log.Write($"Exmo=>loaded #{transactions.Length} wallet transactions");
                }
                _lastCheckedDate = start.AddDays(i);
            }
        }
    }
}