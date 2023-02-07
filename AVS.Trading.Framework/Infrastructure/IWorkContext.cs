using System;
using System.Linq;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Framework.Utils;
using AVS.Trading.Core;
using AVS.Trading.Core.Domain;

namespace AVS.Trading.Framework.Infrastructure
{
    public interface IWorkContext
    {
        string Exchange { get; set; }
        ExchangeClient Client { get; }
        void SwitchAccount(ApiKey key);
        ExchangePair GetExchangePair(string filtersMarket);
    }

    public class WorkContext : IWorkContext
    {
        private readonly ExchangeDirectory _exchangeDirectory;

        public WorkContext(ExchangeDirectory exchangeDirectory)
        {
            _exchangeDirectory = exchangeDirectory;
            Exchange = exchangeDirectory.GetAllExchanges().FirstOrDefault();
            if (string.IsNullOrEmpty(Exchange))
                throw new Exception("WorkContext=> current Exchange is not set. Please check ExchangeDirectory setup");
        }

        public string Exchange { get; set; }

        public ExchangeClient Client => _exchangeDirectory.GetClient(Exchange);

        public void SwitchAccount(ApiKey key)
        {
            if (key == null)
                return;

            Client.Account.Switch(key);
        }

        public ExchangePair GetExchangePair(string market)
        {
            var client = Client;
            return new ExchangePair(client.Pairs.GetPair(market), Exchange, client.Pairs.IsBaseCurrencyFirst);
        }
    }
}