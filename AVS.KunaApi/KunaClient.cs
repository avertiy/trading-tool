using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.ClientApi.WebClients;
using AVS.CoreLib.Utils;
using AVS.KunaApi.Infrastructure;
using AVS.KunaApi.MarketTools;
using AVS.KunaApi.TradingTools;
using AVS.KunaApi.WalletTools;
using AVS.Trading.Core;
using AVS.Trading.Core.Domain;
using Newtonsoft.Json.Linq;

namespace AVS.KunaApi
{
    public class KunaClient : ExchangeClient
    {
        public override string Exchange => KunaConstants.ExchangeName;
        public override bool SupportMarginTrading => false;
        public new KunaMarketApi Markets { get; }
        public new KunaTradingApi Trading { get; }
        public new KunaWalletApi WalletTools { get; }

        public KunaClient(ApiKey key)
            : base(new KunaAccount(key), new KunaPairProvider())
        {
            Trading = new KunaTradingApi(Account.PrivateApiClient);
            Markets = new KunaMarketApi(Account.PublicApiClient);
            WalletTools = new KunaWalletApi(Account.PrivateApiClient);
            Setup(Markets, Trading, null, WalletTools, null);
        }
    }

    public class KunaAccount : ExchangeAccount
    {
        public KunaAccount(ApiKey key) : base(key)
        {
            PublicApiClient.Options.BaseAddress = "https://kuna.io/api/v2/";
            PublicApiClient.Options.RelativeUrl = "";
            PrivateApiClient = new KunaPrivateClient(key.PublicKey, key.PrivateKey)
            {
                Options =
                {
                    BaseAddress = "https://kuna.io/api/v2/",
                    RelativeUrl = "",
                    DefaultVerb = "GET"
                }
            };
        }
    }

    public class KunaPrivateClient : PrivateApiWebClient
    {
        public KunaPrivateClient(string publicKey, string privateKey) : base(publicKey, new HMACSHA256(Encoding.ASCII.GetBytes(privateKey)))
        {
        }

        protected override void OnRequestCreating(string url, RequestData data, string command, string method)
        {
            data.Add("access_key", Authenticator.PublicKey);
            data.Add("tonce", NonceHelper.GetTonce());
            var uri = new Uri(url);
            // "HTTP-verb|URI|params"
            var msg = $"{method}|{uri.AbsolutePath}|{data.ToHttpQueryString()}";
            Authenticator.GetBytes(msg, out string signature);
            data.Add("signature", signature);
        }

        protected override void OnRequestCreated(HttpWebRequest request, RequestData data)
        {
            if (request.Method == "POST")
            {
                var bytes = Encoding.GetBytes(data.ToHttpQueryString());
                request.WriteBytes(bytes);
            }
        }
    }
}