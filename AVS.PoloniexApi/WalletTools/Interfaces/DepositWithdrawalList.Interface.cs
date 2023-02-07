using System.Collections.Generic;
using Jojatekok.PoloniexAPI.WalletTools;
using AVS.PoloniexApi.WalletTools.Models;

namespace AVS.PoloniexApi.WalletTools.Interfaces
{
    public interface IDepositWithdrawalList
    {
        IList<Deposit> Deposits { get; }
        IList<Withdrawal> Withdrawals { get; }
    }
}
