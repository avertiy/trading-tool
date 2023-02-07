using System.Collections.Generic;
using System.Net;
using System.Text;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.Extensions;
using AVS.CoreLib.Utils;

namespace AVS.BinanceApi
{
    public class BinanceApiWebClient
    {
        // API settings
        private Authenticator Authenticator { get; set; }
        private string _url = "https://api.Binance.io/v2/{0}";
        
        public BinanceApiWebClient(Authenticator authenticator)
        {
            Authenticator = authenticator;
            Authenticator.Encoding = Encoding.UTF8;
        }

        public string Execute(string command, IDictionary<string, string> postData)
        {
            using (var wb = new WebClient())
            {
                postData.Add("nonce", NonceHelper.GetNonce());
                var message = postData.ToHttpPostString();

                wb.Headers.Add("Key", Authenticator.PublicKey);
                wb.Headers.Add("Sign", Authenticator.Sign(message).ToLowerInvariant());
                var data = postData.ToNameValueCollection();
                var response = wb.UploadValues(string.Format(_url, command), "POST", data);
                return Encoding.UTF8.GetString(response);
            }
        }
    }
}
