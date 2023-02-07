using System.Linq;
using AVS.Poloniex.Framework.Infrastructure;

namespace AVS.Poloniex.Framework.Utils
{
    public class ApiKeysManager
    {
        private readonly TradingAppConfig _config;

        public ApiKeysManager(TradingAppConfig config)
        {
            _config = config;
        }

        public string[] GetAllAccounts(string exchange)
        {
            return _config.Keys.Where(k => k.Exchange == exchange).Select(k => k.Account).ToArray();
        }

        public ApiKey GetApiKey(string exchange, string account)
        {
            var key = _config.Keys.FirstOrDefault(k => k.Exchange == exchange && k.Account == account);
            return key;
        }

        public ApiKey GetPrimaryApiKey(string exchange)
        {
            var key = _config.Keys.First(k => k.Exchange == exchange && k.IsPrimary);
            return key;
        }
    }
}