using System;

namespace AVS.Trading.Core
{
    public static class Constants
    {
        public const double DefaultReduceKoefficient = 0.1;

        public const double OneSatoshi = 0.00000001;

        /// <summary>
        /// задёт диапозон цены в пределах котрого мы смотрим стакан, к примеру: текущая цена +-60%
        /// </summary>
        public const double OrderBookPriceCutKoefficient = 0.6;

        public const int PricePrecisionDigits = 8;
        public static DateTime RegisterAccountDate = new DateTime(2017,12,1);

        /// <summary>
        ///Poloniex has threshold 0.0001 BTC to place an order
        /// </summary>
        private const double OrderAmountThreshold = 0.0001;

        public static double GetOrderMinAmountThreshold(string baseCurrency)
        {
            switch (baseCurrency)
            {
                case "BTC":
                {
                    return OrderAmountThreshold;
                }
                default: throw new NotSupportedException("Min order amount threshold is not defined for "+baseCurrency);
            }
        }
        
        public static string[] PoloniexMarginCurrencies=> new string[]{"ATOM","DASH","ETH","EOS","LTC","STR","USDT","USDC","XMR","XRP", "DOGE" };
    }
}