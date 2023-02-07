using System;
using System.Collections.Generic;
using AVS.Trading.Core.Interfaces.WalletTools;
using Jojatekok.PoloniexAPI.WalletTools;

namespace AVS.PoloniexApi.WalletTools.Interfaces
{
    public interface IPoloniexWalletApi: IWalletApi
    {
        IDictionary<string, string> GetDepositAddresses();
        IDepositWithdrawalList GetDepositsAndWithdrawals(DateTime startTime, DateTime endTime);
        IGeneratedDepositAddress PostGenerateNewDepositAddress(string currency);
        void PostWithdrawal(string currency, double amount, string address, string paymentId);
    }
}
