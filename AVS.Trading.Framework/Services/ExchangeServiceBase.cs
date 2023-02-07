using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Core;

namespace AVS.Trading.Framework.Services
{
    //Bridge pattern:
    //service.Client = new PoloniexClient();
    //service.GetTicker(...);
    //service.GetTrades(...);
    //etc.
    //service.Client = new ExmoClient();
    //service.GetTicker(...);
    //etc.

    public abstract class ExchangeServiceBase
    {
        private readonly IWorkContext _workContext;
        private ExchangeClient _client;

        /// <summary>
        /// set ExchangeClient to target a certain exhange api
        /// when swith exchange through WorkContext might influence other services in parallel execution
        /// </summary>
        public ExchangeClient Client
        {
            get => _client ?? _workContext.Client;
            set => _client = value;
        }

        protected ExchangeServiceBase(IWorkContext workContext)
        {
            _workContext = workContext;
        }
    }
}