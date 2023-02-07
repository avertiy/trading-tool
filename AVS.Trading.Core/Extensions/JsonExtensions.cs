using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using AVS.Trading.Core.Enums;
using Newtonsoft.Json;

namespace AVS.Trading.Core.Extensions
{
    public static class JsonExtensions
    {
        public static OrderType ToOrderType(this string value)
        {
            switch (value)
            {
                case "buy":
                    return OrderType.Buy;

                case "sell":
                    return OrderType.Sell;
            }

            throw new ArgumentOutOfRangeException("value");
        }

        public static OrderKind ToOrderKind(this string value)
        {
            switch (value)
            {
                case "limit":
                    return OrderKind.Limit;

                case "market":
                    return OrderKind.Market;
            }

            throw new ArgumentOutOfRangeException(nameof(value));
        }

        public static PositionType ToMarginPosition(this string value)
        {
            switch (value)
            {
                case "short":
                    return PositionType.Short;
                case "long":
                    return PositionType.Long;
                default:
                    throw new ArgumentOutOfRangeException("value");
            }
        }

        public static TradeType ToTradeType(this string value)
        {
            switch (value)
            {
                case "bid":
                case "buy":
                    return TradeType.Buy;
                case "ask":
                case "sell":
                    return TradeType.Sell;
            }
            throw new ArgumentException($"{value} unknown TradeType");
        }

        public static TradeCategory ToTradeCategory(this string value)
        {
            switch (value)
            {
                case "exchange":
                    return TradeCategory.Exchange;
                case "marginTrade":
                    return TradeCategory.MarginTrade;
                case "settlement":
                    return TradeCategory.Settlement;
                case "lendingFees":
                    return TradeCategory.LendingFees;
                default:
                    throw new ArgumentOutOfRangeException("value: " + value);
            }
        }
    }
}