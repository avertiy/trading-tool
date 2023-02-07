using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.ClientApi.WebClients;
using AVS.CoreLib.Utils;
using AVS.CoreLib._System;
using Jojatekok.PoloniexAPI.WalletTools;
using AVS.PoloniexApi.WalletTools.Interfaces;

namespace AVS.PoloniexApi.WalletTools
{
    public class WalletApiAsync : PoloniexWalletApi
    {
        public WalletApiAsync(PrivateApiWebClient apiWebClient) : base(apiWebClient)
        {
        }


        public Task<IDictionary<string, string>> GetDepositAddressesAsync()
        {
            return Task.Factory.StartNew(GetDepositAddresses);
        }

        public Task<IDepositWithdrawalList> GetDepositsAndWithdrawalsAsync(DateTime startTime, DateTime endTime)
        {
            return Task.Factory.StartNew(() => GetDepositsAndWithdrawals(startTime, endTime));
        }

        public Task<IDepositWithdrawalList> GetDepositsAndWithdrawalsAsync()
        {
            return Task.Factory.StartNew(() => GetDepositsAndWithdrawals(UnixEpoch.DateTimeUnixEpochStart, DateTime.MaxValue));
        }

        public Task<IGeneratedDepositAddress> PostGenerateNewDepositAddressAsync(string currency)
        {
            return Task.Factory.StartNew(() => PostGenerateNewDepositAddress(currency));
        }

        public Task PostWithdrawalAsync(string currency, double amount, string address, string paymentId)
        {
            return Task.Factory.StartNew(() => PostWithdrawal(currency, amount, address, paymentId));
        }

        public Task PostWithdrawalAsync(string currency, double amount, string address)
        {
            return Task.Factory.StartNew(() => PostWithdrawal(currency, amount, address, null));
        }
    }
}