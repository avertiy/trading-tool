using System.Collections.Generic;
using System.Threading.Tasks;
using AVS.CoreLib._System.Net;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.WalletTools;
using AVS.Trading.Core.ResponseModels;

namespace AVS.Trading.Framework.Services.WalletTools
{
    public interface IWalletToolsService
    {
        /// <summary>
        /// Returns all of your balances, including available balance, balance on orders, 
        /// and the estimated BTC value of your balance. 
        /// </summary>
        /// <param name="all">false - balances for Exchange account, true = balances for all accounts
        /// </param>
        Response<IDictionary<string, IBalance>> GetCompleteBalances(bool all = false);
        SimpleResponse TransferBalance(string currency, double amount, AccountType from, AccountType to);

        Task<Response<IDictionary<string, IBalance>>> GetCompleteBalancesAsync();
    }

    public class WalletToolsService : ExchangeServiceBase, IWalletToolsService
    {
        protected IWalletApi Wallet => Client.WalletTools;

        public WalletToolsService(IWorkContext workContext) : base(workContext)
        {
        }
        
        public Response<IDictionary<string, IBalance>> GetCompleteBalances(bool all = false)
        {
            return Client.WalletTools.GetCompleteBalances(all);
        }

        public SimpleResponse TransferBalance(string currency, double amount, AccountType from, AccountType to)
        {
            return Client.WalletTools.TransferBalance(currency, amount, from, to);
        }

        public Task<Response<IDictionary<string, IBalance>>> GetCompleteBalancesAsync()
        {
            return Client.WalletTools.GetCompleteBalancesAsync();
        }
    }
}
