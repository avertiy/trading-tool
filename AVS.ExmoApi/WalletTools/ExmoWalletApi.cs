using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AVS.CoreLib._System;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.ClientApi.WebClients;
using AVS.CoreLib._System.Net;
using AVS.CoreLib.Extensions;
using AVS.CoreLib.Utils;
using AVS.ExmoApi.Models;
using AVS.ExmoApi.TradingTools.Models;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.WalletTools;
using AVS.Trading.Core.ResponseModels;

namespace AVS.ExmoApi.WalletTools
{
    public class ExmoWalletApi : ApiToolsBase, IWalletApi
    {
        public ExmoWalletApi(PrivateApiWebClient apiWebClient) : base(apiWebClient)
        {
        }

        public Response<IDictionary<string, IBalance>> GetCompleteBalances(bool all)
        {
            throw new System.NotImplementedException();
        }

        public SimpleResponse TransferBalance(string currency, double amount, AccountType @from, AccountType to)
        {
            throw new System.NotImplementedException();
        }

        public async Task<Response<IDictionary<string, IBalance>>> GetCompleteBalancesAsync()
        {
            //user_info
            //{
            //    "uid": 10542,
            //    "server_date": 1435518576,
            //    "balances": {
            //        "BTC": "970.994",
            //        "USD": "949.47"
            //    },
            //    "reserved": {
            //        "BTC": "3",
            //        "USD": "0.5"
            //    }
            //}
            var jsonResult = await ExecuteAsync("user_info", RequestData.Empty);
            var response = jsonResult.AsObject<ExmoUserInfo>().Map<ExmoUserInfo>();

            var result = response.OnSuccess<IDictionary<string, IBalance>>(() =>
            {
                var dict = new Dictionary<string, IBalance>();
                foreach (var kp in response.Data.Balances)
                    dict.Add(kp.Key, new ExmoBalance()
                    {
                        Currency = kp.Key,
                        QuoteAvailable = kp.Value,
                        QuoteOnOrders = response.Data.Reserved[kp.Key]
                    });
                return dict;
            });

            return result;
        }

        public WalletHistory GetWalletHistory(DateTime date)
        {
            var postData = new Dictionary<string, object> {
                { "date", date.DateTimeToUnixTimeStamp()}
            };
            var data = Execute("wallet_history", postData).Deserialize<WalletHistory>();
            return data;
        }

        /// <summary>
        /// the date range should be not more 10 days - only 10 request are allowed per minute
        /// </summary>
        public WalletHistory GetWalletHistory(DateTime from, DateTime to)
        {
            var walletHistory = GetWalletHistory(from.Date);
            if (!walletHistory.Success)
                return walletHistory;
            if ((to - from).TotalDays > 10)
                throw new ArgumentOutOfRangeException("maximum 10 days range is allowed due to only 10 request are allowed per minute");
            foreach (var date in DateExtensions.EachDay(@from.Date.AddDays(1), to))
            {
                var history = GetWalletHistory(date);
                if (history.Success && history.Items.Count > 0)
                {
                    walletHistory.Items.AddRange(history.Items);
                    walletHistory.To = history.To;
                }
            }

            return walletHistory;
        }
    }
}