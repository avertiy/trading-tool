using AVS.ProxyUtil;

namespace AVS.Trading.Tool.Forms
{
    public class MainFormController
    {
        public string PingPoloniex()
        {
            return ProxyHelper.SendTestWebRequest("https://poloniex.com/public?command=returnOrderBook&currencyPair=BTC_ETH&depth=1", true);
        }
    }
}