using System;

namespace AVS.Trading.Core.Interfaces.WalletTools
{
    public interface IBalance
    {
        /// <summary>
        /// Number of tokens not reserved in orders.
        /// </summary>
        double QuoteAvailable { get; }
        /// <summary>
        /// Number of tokens in open orders.
        /// </summary>
        double QuoteOnOrders { get; }
        double BitcoinValue { get; }
        string Currency { get; set; }
        bool IsEmpty { get; }
    }
}
