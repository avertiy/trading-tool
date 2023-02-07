using System.Collections.Generic;
using Jojatekok.PoloniexAPI.WalletTools;
using Newtonsoft.Json;
using AVS.PoloniexApi.WalletTools.Interfaces;

namespace AVS.PoloniexApi.WalletTools.Models
{
    public class DepositWithdrawalList : IDepositWithdrawalList
    {
        [JsonProperty("deposits")]
        public IList<Deposit> Deposits { get; private set; }

        [JsonProperty("withdrawals")]
        public IList<Withdrawal> Withdrawals { get; private set; }
    }
}
