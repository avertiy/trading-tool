using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AVS.Trading.Tool.Models.Trading;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces;
using AVS.Trading.Core.Interfaces.MarketTools;

namespace AVS.Trading.Tool.Models
{
    /// <summary>
    /// Trade summary for a period (day, week, month etc.)
    /// </summary>
    public class TradeSummary 
    {
        #region prop-s
        private double _profitLoss;

        public string Pair { get; set; }

        public DateTime FromUtc { get; set; }
        public DateTime ToUtc { get; set; }

        public TradeTotals Exchange { get; set; }
        public MarginTradeTotals Margin { get; set; }

        /// <summary>
        /// in base currency 
        /// </summary>
        public double ProfitLoss
        {
            get
            {
                if (_profitLoss <= 0.0 && !Exchange.IsEmpty && !Margin.IsEmpty)
                    _profitLoss = Exchange.ProfitLossTotal + Margin.ProfitLossTotal;
                return _profitLoss;
            }
            set => _profitLoss = value;
        } 
        #endregion

        public TradeSummary()
        {
            Exchange = new TradeTotals();
            Margin = new MarginTradeTotals();
        }

        public override string ToString()
        {
            var pair = CurrencyPair.Parse(Pair);
            return $"{Pair} [profit/loss - {ProfitLoss:##.#####}{pair.BaseCurrency}]";
        }

        public string GetInfo(bool quote = true)
        {
            var sb = new StringBuilder();
            if (!Exchange.IsEmpty)
            {
                if (!Margin.IsEmpty)
                    sb.Append($"Exchange: ");
                sb.Append(Exchange.GetInfo(quote, Pair));
                sb.Append($"   ");
            }
            if (!Margin.IsEmpty)
            {
                if (!Exchange.IsEmpty)
                    sb.Append($"Margin: ");
                sb.Append(Margin.GetInfo(quote, Pair));
            }
            return sb.ToString();
        }

        public string GetAvgPriceInfo()
        {
            var sb = new StringBuilder();
            var pair = CurrencyPair.Parse(Pair);
            if (!Exchange.IsEmpty)
            {
                if (!Margin.IsEmpty)
                    sb.Append($"Exchange: ");
                if(Exchange.AvgBuyPrice > 0)
                    sb.Append($"Avg. buy price: {Exchange.AvgBuyPrice.FormatNumber(pair.BaseCurrency)}  ");
                if (Exchange.AvgSellPrice > 0)
                    sb.Append($"Avg. sell price: {Exchange.AvgSellPrice.FormatNumber(pair.BaseCurrency)}  ");
                sb.Append($"   ");
            }
            if (!Margin.IsEmpty)
            {
                if (!Exchange.IsEmpty)
                    sb.Append($"Margin: ");
                if (Margin.AvgBuyPrice > 0)
                    sb.Append($"Avg. buy price: {Margin.AvgBuyPrice.FormatNumber(pair.BaseCurrency)}  ");
                if (Margin.AvgSellPrice > 0)
                    sb.Append($"Avg. sell price: {Margin.AvgSellPrice.FormatNumber(pair.BaseCurrency)}");
            }
            return sb.ToString();
        }
        
        public void Initialize(IEnumerable trades)
        {
            var list = new List<IMarketTradeItem>();
            foreach (var trade in trades)
            {
                if (trade is IMarketTradeItem item)
                    list.Add(item);
                else
                    throw new ArgumentException("ITradeItem item is expected");
            }

            Initialize(list);
        }

        public void Initialize(IList<IMarketTradeItem> trades)
        {
            if (trades == null || trades.Count == 0)
                return;

            var trade = trades.First();
            
            Pair = trade.Pair;
            FromUtc = trades.Min(t => t.DateUtc);
            ToUtc = trades.Max(t => t.DateUtc);
            

            if (trades.Any(t => t.Pair != Pair))
                Pair = CurrencyPair.All;

            if (trade is ITradeCategory)
            {
                var exchange = trades.Where(t => ((ITradeCategory)t).Category == TradeCategory.Exchange).ToList();
                var margin = trades.Where(t => ((ITradeCategory)t).Category == TradeCategory.MarginTrade).ToList();
                var settlement = trades.Where(t => ((ITradeCategory)t).Category == TradeCategory.Settlement).ToList();
                Exchange.Initialize(exchange);
                Margin.Initialize(margin);
                Margin.AddSettlements(settlement);
            }
            else
            {
                //market trades=> all trades are exhange
                Exchange.Initialize(trades);
            }
        }
    }
}