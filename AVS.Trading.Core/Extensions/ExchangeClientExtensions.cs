namespace AVS.Trading.Core
{
    public static class ExchangeClientExtensions
    {
        public static bool SupportWalletTools(this ExchangeClient client)
        {
            try
            {
                if (client.WalletTools != null)
                    return true;
            }
            catch { }
            return false;
        }

        public static bool SupportMarginTools(this ExchangeClient client)
        {
            try
            {
                if (client.MarginTools != null)
                    return true;
            }
            catch { }
            return false;
        }
    }
}