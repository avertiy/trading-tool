using System;
using AVS.CoreLib.Data;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.MarketTools;

namespace AVS.Trading.Data.Domain.MarketTools.TradeHistory
{
    public class MarketTradeItem : BaseEntity, IMarketTradeItem
    {
        public string Pair { get; set; }
        public DateTime DateUtc { get; set; }
        public TradeType Type { get; set; }
        public double Price { get; set; }
        public double AmountQuote { get; set; }
        public double AmountBase { get; set; }

        public override string ToString()
        {
            return
                $"MarketTradeItem=> {Pair} {Type} {AmountQuote.FormatAsQuantity()}x{Price.FormatAsPrice()}={AmountBase.FormatNumber()}";
        }
    }
}
