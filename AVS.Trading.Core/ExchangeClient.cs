using System;
using System.Diagnostics;
using System.Security.Cryptography;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.Json;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Interfaces;
using AVS.Trading.Core.Interfaces.LendingTools;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Core.Interfaces.WalletTools;

namespace AVS.Trading.Core
{
    public abstract class ExchangeClient
    {
        private bool _setupHasBeenDone = false;
        public ExchangeAccount Account { get; protected set; }

        public abstract bool SupportMarginTrading { get; }

        public IPairProvider Pairs { get; protected set; }
        public abstract string Exchange { get; }
        [DebuggerHidden]
        public IMarketApi MarketTools { get; protected set; }
        [DebuggerHidden]
        public ITradingApi TradingTools { get; protected set; }
        [DebuggerHidden]
        public IMarginTradingApi MarginTools { get; protected set; }
        [DebuggerHidden]
        public IWalletApi WalletTools { get; protected set; }
        [DebuggerHidden]
        public ILendingApi LendingTools { get; protected set; }
        
        protected ExchangeClient(ExchangeAccount account, IPairProvider provider)
        {
            account.Validate();
            Account = account;
            Pairs = provider;
        }
        
        public void Validate()
        {
            if(!_setupHasBeenDone)
                throw new Exception($"{this.GetType().Name} has not been setup [call Setup method in c-tor of inheritor]");
            if(Pairs == null)
                throw new Exception($"{this.GetType().Name} has not been setup [Pair provider is not set]");
        }
        
        public virtual JsonResponseResult ExecuteCommand(string command, bool @private = true)
        {
            if(@private)
                return this.Account.ExecutePrivateCommand(command);
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return $"Client public api: {Account.PublicApiClient}; private api: {Account.PrivateApiClient}";
        }

        protected void Setup(IMarketApi markets, ITradingApi tradingApi, IMarginTradingApi margin, IWalletApi wallet, ILendingApi lendingApi)
        {
            MarketTools = markets;
            TradingTools = tradingApi;
            MarginTools = margin;
            WalletTools = wallet;
            LendingTools = lendingApi;
            _setupHasBeenDone = true;
        }
    }

    public class ExchangeAccount: BaseApiClient
    {
        /// <summary>
        /// Exchange account name  or alias to switch between accounts
        /// </summary>
        public string Name { get; protected set; }
        
        public ExchangeAccount(ApiKey key) : base(key.PublicKey, key.PrivateKey)
        {
            Name = key.Name;
        }

        public void Switch(ApiKey key)
        {
            PrivateApiClient.SwitchKeys(key.PublicKey, key.PrivateKey);
            Name = key.Name;
        }

        public void Validate()
        {
            PublicApiClient.Validate();
            PrivateApiClient.Validate();
        }
    }
}