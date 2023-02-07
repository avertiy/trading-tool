using System.Collections.Generic;
using System.Net;
using System.Text;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.Extensions;
using AVS.CoreLib.Utils;

namespace AVS.ExmoApi
{
    public class ExmoApiWebClient
    {
        // API settings
        private Authenticator Authenticator { get; set; }
        private string _url = "http://api.exmo.com/v1/{0}";
        
        public ExmoApiWebClient(Authenticator authenticator)
        {
            authenticator.Encoding = Encoding.UTF8;
            Authenticator = authenticator;
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
