using System.Collections.Generic;
using System.Threading.Tasks;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.ResponseModels;

namespace AVS.Trading.Core.Interfaces.WalletTools
{
    public interface IWalletApi
    {
        Response<IDictionary<string, IBalance>> GetCompleteBalances(bool all);
        SimpleResponse TransferBalance(string currency, double amount, AccountType @from, AccountType to);
        Task<Response<IDictionary<string, IBalance>>> GetCompleteBalancesAsync();
    }
}