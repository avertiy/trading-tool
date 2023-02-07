using System.Collections.Generic;
using System.Threading.Tasks;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.ClientApi.WebClients;
using AVS.CoreLib._System.Net;
using AVS.CoreLib.Utils;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.WalletTools;
using AVS.Trading.Core.ResponseModels;

namespace AVS.KunaApi.WalletTools
{
    public class KunaWalletApi : ApiToolsBase, IWalletApi
    {
        public KunaWalletApi(PrivateApiWebClient apiWebClient) : base(apiWebClient)
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
            var jsonResult = await ExecuteAsync("members/me", RequestData.Empty);
            var response = jsonResult.AsObject<KunaUserInfo>().Map<KunaUserInfo>();

            var result = response.OnSuccess<IDictionary<string, IBalance>>(() =>
            {
                var dict = new Dictionary<string, IBalance>();
                foreach (var item in response.Data.Balances)
                    dict.Add(item.Currency, item);
                return dict;
            });

            return result;
        }
    }
}