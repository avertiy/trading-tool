namespace AVS.PoloniexApi.PoloniexCommands
{
    public static class WalletCommands
    {
        /// <summary>
        /// Returns all of your balances, including available balance, balance on orders, 
        /// and the estimated BTC value of your balance. 
        /// By default, this call is limited to your exchange account; 
        /// set the "account" POST parameter to "all" to include your margin and lending accounts.
        /// Sample output: {"LTC":{"available":"5.015","onOrders":"1.0025","btcValue":"0.078"},"NXT:{...} ... }
        /// ВОЗВРАЩАЕТ ТО ЧТО ЛЕЖИТ В КОЛЛАТЕРАЛЕ НА МАРЖИН АККАУНТЕ
        /// </summary>
        public const string ReturnCompleteBalances = "returnCompleteBalances";

        public const string TransferBalance = "transferBalance";

        public const string ReturnDepositAddresses = "returnDepositAddresses";
        public const string ReturnDepositsWithdrawals = "returnDepositsWithdrawals";
        public const string ReturnMarginAccountSummary = "returnMarginAccountSummary";

        
        
    }
}