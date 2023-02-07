using System;
using System.Collections.Generic;
using System.Linq;
using AVS.Trading.Core;

namespace AVS.Trading.Framework.Utils
{
    /// <summary>
    /// pairs container
    /// </summary>
    public class ExchangeDirectory
    {
        private readonly Dictionary<string, ExchangeClient> _exchanges = new Dictionary<string, ExchangeClient>();

        public void Register(ExchangeClient client)
        {
            _exchanges[client.Exchange] = client;
        }

        public string[] GetAllExchanges()
        {
            return _exchanges.Keys.ToArray();
        }

        public ExchangeClient GetClient(string exchange)
        {
            if(!_exchanges.ContainsKey(exchange))
                throw new ArgumentException($"Exchange {exchange} has not been registered");
            return _exchanges[exchange];
        }

        public ExchangeClient[] GetAllClients()
        {
            return _exchanges.Values.ToArray();
        }
    }
}