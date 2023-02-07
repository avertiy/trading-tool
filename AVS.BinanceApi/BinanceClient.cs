using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AVS.CoreLib.ClientApi.WebClients;
using AVS.CoreLib.Utils;
using AVS.BinanceApi.Infrastructure;
using AVS.BinanceApi.MarketTools;
using AVS.BinanceApi.TradingTools;
using AVS.BinanceApi.WalletTools;
using AVS.Trading.Core;
using AVS.Trading.Core.Domain;

namespace AVS.BinanceApi
{
    public class BinanceClient : ExchangeClient
    {
        public override string Exchange => BinanceConstants.ExchangeName;
        public override bool SupportMarginTrading => false;
        public new BinanceMarketApi Markets { get; }
        public new BinanceTradingApi Trading { get; }
        public new BinanceWalletApi WalletTools { get; }

        public BinanceClient(ApiKey key)
            : base(new BinanceAccount(key), new BinancePairProvider())
        {
            Trading = new BinanceTradingApi(Account.PrivateApiClient);
            Markets = new BinanceMarketApi(Account.PublicApiClient);
            WalletTools = new BinanceWalletApi(Account.PrivateApiClient);
            Setup(Markets, Trading, null, WalletTools, null);
        }
    }

    public class BinanceAccount : ExchangeAccount
    {
        public BinanceAccount(ApiKey key) : base(key)
        {
            
            PublicApiClient.Options.BaseAddress = "https://api.binance.com/api/v3/";
            PublicApiClient.Options.RelativeUrl = "";
            PrivateApiClient = new BinancePrivateClient(key.PublicKey, key.PrivateKey)
            {
                Options =
                {
                    BaseAddress = "https://api.binance.com/api/v3/",
                    RelativeUrl = "",
                    DefaultVerb = "GET"
                }
            };
        }
    }

    public class BinancePrivateClient : PrivateApiWebClient
    {
        HMACSHA256 _hmac;
        public BinancePrivateClient(string publicKey, string privateKey) : base(publicKey, new HMACSHA256(Encoding.UTF8.GetBytes(privateKey)))
        {
            Encoding = Encoding.UTF8;
            _hmac = new HMACSHA256(Encoding.UTF8.GetBytes(privateKey));
        }
        
        protected override void OnRequestCreating(string url, RequestData data, string command, string method)
        {
            data.Add("timestamp", NonceHelper.GetTonce());
            data.Add("recvWindow", "60000");
            var msg = $"{data.ToHttpQueryString()}";
            Authenticator.GetBytes(msg, out string signature);
            data.Add("signature", signature);
        }

        protected override void OnRequestCreated(HttpWebRequest request, RequestData data)
        {
            request.Headers.Add("X-MBX-APIKEY", Authenticator.PublicKey);
            if (request.Method == "POST")
            {
                var bytes = Encoding.GetBytes(data.ToHttpQueryString());
                request.WriteBytes(bytes);
            }
        }

        private static string FetchResponseAttempt(HttpWebRequest request)
        {
            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        if (responseStream == null)
                            throw new NullReferenceException("The HttpWebRequest's response stream cannot be empty.");
                        using (StreamReader streamReader = new StreamReader(responseStream))
                            return streamReader.ReadToEnd();
                    }
                }
            }
            catch (Exception ex)
            {
                return string.Format("{{ \"error\": \"Request to {0} failed. {1}\" }}", (object)request.RequestUri, (object)ex.Message);
            }
        }
    }
}