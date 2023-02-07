using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Formatters;
using AVS.Trading.Core.Interfaces;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Data.Domain.TradingTools;

namespace AVS.Trading.Tool.Models
{
    public class TradeTotals
    {
        #region Prop-s
        public string[] Markets { get; set; }
        /// <summary>
        /// in Base currency
        /// </summary>
        public double Buys { get; set; }
        /// <summary>
        /// in Base currency
        /// </summary>
        public double AvgBuyVolume { get; set; }
        /// <summary>
        /// in Base currency
        /// </summary>
        public double Sells { get; set; }

        /// <summary>
        /// in Base currency
        /// </summary>
        public double AvgSellVolume { get; set; }

        /// <summary>
        /// in quote currecny
        /// </summary>
        public double VolumeBought { get; set; }
        /// <summary>
        /// in quote currency
        /// </summary>
        public double VolumeSold { get; set; }

        public int CountBuyTrades { get; set; }
        public int CountSellTrades { get; set; }

        /// <summary>
        /// in Base currency
        /// </summary>
        public double AvgBuyPrice => VolumeBought > 0 ? Buys / VolumeBought : 0;

        /// <summary>
        /// in Base currency
        /// </summary>
        public double AvgSellPrice => VolumeSold > 0 ? Sells / VolumeSold : 0;

        public virtual double ProfitLossTotal => Sells - Buys;

        public virtual double ProfitLossAmount => VolumeBought > VolumeSold ? VolumeSold : VolumeBought;
        public virtual double ProfitLossSubtotal => ProfitLossAmount * (AvgSellPrice - AvgBuyPrice);

        public virtual double Volume => VolumeBought - VolumeSold;

        public bool IsEmpty => VolumeBought + VolumeSold + Sells + AvgBuyVolume == 0; 
        #endregion
        
        public virtual void Initialize(IList<IMarketTradeItem> trades)
        {
            var buys = trades.Where(t => t.Type == TradeType.Buy).ToArray();
            var sells = trades.Where(t => t.Type == TradeType.Sell).ToArray();

            Markets = trades.Select(t => t.Pair).Distinct().ToArray();

            InitializeBuys(buys);
            InitializeSells(sells);
        }

        protected void InitializeBuys(IMarketTradeItem[] buys)
        {
            CountBuyTrades = buys.Length;
            if (buys.Any())
            {
                
                //if only one market
                if (buys.Any(b => b.Pair == buys[0].Pair))
                {
                    VolumeBought = buys.Sum(b => b.AmountQuote);
                    AvgBuyVolume = buys.Average(b => b.AmountBase);
                }
                Buys = buys.Sum(b => b.AmountBase);
            }
        }

        protected void InitializeSells(IMarketTradeItem[] sells)
        {
            CountSellTrades = sells.Length;
            if (sells.Any())
            {
                if (sells.Any(b => b.Pair == sells[0].Pair))
                {
                    VolumeSold = sells.Sum(b => b.AmountQuote);
                    AvgSellVolume = sells.Average(b => b.AmountBase);
                }
                Sells = sells.Sum(b => b.AmountBase);
            }
        }
        
        public void Add(TradeType type, double amountQuote, double amountBase)
        {
            if (type == TradeType.Buy)
            {
                VolumeBought += amountQuote;
                Buys += amountBase;
            }else if (type == TradeType.Sell)
            {
                VolumeSold += amountQuote;
                Sells += amountBase;
            }
        }

        public override string ToString()
        {
            if (Markets.Length == 0)
                return "";
            if (Markets.Length == 1)
            {
                var pair = CurrencyPair.Parse(Markets[0]);
                var buysStr = TradingFormatter.Format($"Buys: {VolumeBought} {pair:q} {Buys} {pair:b}");
                var sellsStr = TradingFormatter.Format($"Sells: {VolumeSold} {pair:q} {Sells} {pair:b}");
                return TradingFormatter.Format($"{pair:market} P&L:{ProfitLossTotal} {pair:b} {buysStr} {sellsStr}");
            }
            return $"P&L: {ProfitLossTotal.FormatNumber("*BTC")}";
        }

        public string GetInfo(bool quote, string market)
        {
            if (string.IsNullOrEmpty(market) || market == CurrencyPair.All)
                return string.Empty;
            var sb = new StringBuilder();

            var pair = CurrencyPair.Parse(market);
            
            var baseCurrency = pair.BaseCurrency;
            var quoteCurrency = pair.QuoteCurrency;
            if (quote)
            {
                if (VolumeBought > 0)
                    sb.Append("+" + VolumeBought.FormatNumber(quoteCurrency)+"   ");
                if (VolumeSold > 0)
                    sb.Append("-" + VolumeSold.FormatNumber(quoteCurrency)+"   ");
                if (VolumeBought > 0 && VolumeSold > 0)
                    sb.Append("Total " + Volume.FormatNumber(quoteCurrency)+"   ");
            }
            else
            {
                if (Buys > 0)
                    sb.Append("+" + Buys.FormatNumber(baseCurrency) + "   ");
                if (Sells > 0)
                    sb.Append("-" + Sells.FormatNumber(baseCurrency) + "   ");

                if (Buys > 0 && Sells > 0)
                    sb.Append("Total " + ProfitLossTotal.FormatNumber(baseCurrency) + "   ");
            }

            return sb.ToString();
        }
    }
    
    public class MarginTradeTotals : TradeTotals
    {
        /// <summary>
        /// in Base currency
        /// </summary>
        public double LendingFees { get; set; }
        /// <summary>
        /// in Base currency
        /// </summary>
        public double SettlementTotal { get; set; }

        public double SettlementAmount { get; set; }

        public override double ProfitLossTotal => Sells - Buys - LendingFees - SettlementTotal;
        
        protected void InitializeSettlements(TradeItem[] settlement)
        {
            if (settlement.Any())
            {
                SettlementTotal = settlement.Sum(s => s.AmountBase);
                if (settlement.Any(b => b.Pair == settlement[0].Pair))
                    SettlementAmount = settlement.Sum(s => s.AmountQuote);
            }
        }

        public void AddSettlements(IList<IMarketTradeItem> settlements)
        {
            if (settlements.Any())
            {
                SettlementTotal = settlements.Sum(s => s.AmountBase);
                if (settlements.Any(b => b.Pair == settlements[0].Pair))
                    SettlementAmount = settlements.Sum(s => s.AmountQuote);
            }
        }

        public void AddSettlement(TradeType type, double amountQuote, double amountBase)
        {
            if (type == TradeType.Buy)
            {
                SettlementAmount += amountQuote;
                SettlementTotal += amountBase;
            }
            else
            {
                throw new Exception("settlement sell type is unknown");
            }
        }
    }
}