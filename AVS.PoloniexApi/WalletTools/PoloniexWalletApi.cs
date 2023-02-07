using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.ClientApi.WebClients;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.WalletTools;
using AVS.Trading.Core.ResponseModels;
using Jojatekok.PoloniexAPI.WalletTools;
using AVS.PoloniexApi.PoloniexCommands;
using AVS.PoloniexApi.WalletTools.Interfaces;
using AVS.PoloniexApi.WalletTools.Models;
using AVS.Trading.Core.Extensions;

namespace AVS.PoloniexApi.WalletTools
{
    public class PoloniexWalletApi : ApiToolsBase, IPoloniexWalletApi
    {
        public PoloniexWalletApi(PrivateApiWebClient apiWebClient) : base(apiWebClient)
        {
        }

        public Response<IDictionary<string, IBalance>> GetCompleteBalances(bool all = false)
        {
            var data = RequestData.Empty;
            if(all)
                data.Add("account","all");

            var jsonResult = Execute(WalletCommands.ReturnCompleteBalances, data);
            var response = jsonResult.AsDictionary<string, IBalance>()
                .ForEach((key, value) => value.Currency = key)
                .Map<PoloniexBalance>();

            return response;
        }
        
        public SimpleResponse TransferBalance(string currency, double amount, AccountType from, AccountType to)
        {
            var postData = new Dictionary<string, object>
            {
                {"currency", currency},
                {"amount", amount},
                {"fromAccount", from.ToString().ToLower()},
                {"toAccount", to.ToString().ToLower()}//exchange, margin, lending
            };
            var reponse = Execute(WalletCommands.TransferBalance, postData).Deserialize<SimpleResponse>();
            return reponse;
        }

        public async Task<Response<IDictionary<string, IBalance>>> GetCompleteBalancesAsync()
        {
            var data = RequestData.Empty;
            //if (all)
            //    data.Add("account", "all");

            var jsonResult = await ExecuteAsync(WalletCommands.ReturnCompleteBalances, data);
            var response = jsonResult.AsDictionary<string, IBalance>()
                .ForEach((key, value) => value.Currency = key)
                .Map<PoloniexBalance>();
            return response;
        }


        //public IDictionary<string, IList<IOpenLoanOffer>> GetLendingHistory(DateTime startTime, DateTime endTime, 
        //    int limit = 10000)
        //{
        //    var postData = new Dictionary<string, object> {
        //        { "start", startTime.DateTimeToUnixTimeStamp() },
        //        { "end", endTime.DateTimeToUnixTimeStamp() },
        //        { "limit", limit },
        //    };

        //    //{
        //    //    "id": 175589553,
        //    //    "currency": "BTC",
        //    //    "rate": "0.00057400",
        //    //    "amount": "0.04374404",
        //    //    "duration": "0.47610000",
        //    //    "interest": "0.00001196",
        //    //    "fee": "-0.00000179",
        //    //    "earned": "0.00001017",
        //    //    "open": "2016-09-28 06:47:26",
        //    //    "close": "2016-09-28 18:13:03"
        //    //}

        //    var data = PostData<IList<>>(WalletCommands.ReturnLendingHistory, postData);
        //    return data != null && data.Any() ? data.ToList<ITrade>() : new List<ITrade>();
        //}


        public IDictionary<string, string> GetDepositAddresses()
        {
            var postData = new Dictionary<string, object>();
            var data = Execute(WalletCommands.ReturnDepositAddresses, postData).Deserialize<IDictionary<string, string>>();
            return data;
        }

        public IDepositWithdrawalList GetDepositsAndWithdrawals(DateTime startTime, DateTime endTime)
        {
            var postData = new Dictionary<string, object> {
                { "start", startTime.DateTimeToUnixTimeStamp() },
                { "end", endTime.DateTimeToUnixTimeStamp() }
            };

            var data = Execute(WalletCommands.ReturnDepositsWithdrawals, postData).Deserialize<DepositWithdrawalList>();
            return data;
        }

        public IGeneratedDepositAddress PostGenerateNewDepositAddress(string currency)
        {
            var postData = new Dictionary<string, object> {
                { "currency", currency }
            };

            var data = Execute("generateNewAddress", postData).Deserialize<IGeneratedDepositAddress>();
            return data;
        }

        public void PostWithdrawal(string currency, double amount, string address, string paymentId)
        {
            var postData = new Dictionary<string, object> {
                { "currency", currency },
                { "amount", amount.ToStringNormalized() },
                { "address", address }
            };

            if (paymentId != null)
            {
                postData.Add("paymentId", paymentId);
            }

            var res = Execute("withdraw", postData).Deserialize<IGeneratedDepositAddress>();
        }
    }

    
}
