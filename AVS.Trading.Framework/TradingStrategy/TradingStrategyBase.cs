using System;
using System.Text;
using AVS.Trading.Framework.Adapters;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;

namespace AVS.Trading.Framework.TradingStrategy
{
    public interface ITradingStrategy
    {
        CurrencyPair Market { get; set; }
        TradeCategory TradeCategory { get; set; }
        void Initialize(TradingAppConfig.TradingNode.StrategyNode config);
        string GetInfo();
    }

    public abstract class TradingStrategyBase: ITradingStrategy
    {
        public bool Enabled = true;
        /// <summary>
        /// in quote currency
        /// </summary>
        protected double? OrderAmountLimit;
        protected double? OrderTotalLimit;
        public double MarginThreshold;
        /// <summary>
        /// стратегия для Exchange или Margin или и то и другое
        /// </summary>
        public TradeCategory TradeCategory { get; set;}

        public CurrencyPair Market { get; set; }
        public string Name { get; set; }
        
        public virtual void Initialize(TradingAppConfig.TradingNode.StrategyNode config)
        {
            if(config == null)
                throw new ArgumentNullException("config");
            MarginThreshold = config.MarginThreshold ?? 1.004;//0.25%+0.15%
            OrderAmountLimit = config.OrderAmountLimit;
            OrderTotalLimit = config.OrderTotalLimit;
            Name = config.Type;
            Enabled = config.Enabled;
        }

        public virtual double CheckOrderAmount(double amountQuote, double price)
        {
            //check order amount limit
            if (OrderAmountLimit.HasValue && OrderAmountLimit > 0 && amountQuote > OrderAmountLimit.Value)
                amountQuote = OrderAmountLimit.Value;
            var total = amountQuote * price;

            //if quote amount less than limit order threshold
            if (total < Constants.GetOrderMinAmountThreshold(Market.BaseCurrency))
                amountQuote = amountQuote * 1.01;//add 1% to the amount

            if (OrderTotalLimit.HasValue && OrderTotalLimit > 0 && total > OrderTotalLimit.Value)
                amountQuote = OrderTotalLimit.Value / price;
            return amountQuote;
        }

        public virtual string GetInfo()
        {
            var sb = new StringBuilder();
            sb.Append($"{Name} market {Market} category {TradeCategory}");

            if (OrderAmountLimit.HasValue && OrderAmountLimit.Value > 0)
                sb.Append($"order amount limit {OrderAmountLimit.Value} {Market.QuoteCurrency}");
            if (OrderTotalLimit.HasValue && OrderTotalLimit.Value > 0)
                sb.Append($"order total limit {OrderTotalLimit.Value} {Market.BaseCurrency}");

            return sb.ToString();
        }
        
    }

    

    /*
    public abstract class TradingStrategyBase1 : TradingStrategyBase
    {
        public void Execute()
        {
            MyTradeItem lastTrade = GetLastTrade();
            if (lastTrade == null)
            {
                OrderNotPlaced($"No trades found for {Market}");
                return;
            }

            LogWriter.WriteSystemDetails($"Last trade {lastTrade.ToString()}");

            if (lastTrade.Type == TradeType.Buy)
            {
                ExecuteSellStrategy(lastTrade);
            }
            else
            {
                ExecuteBuyStrategy(lastTrade);
            }
        }

        protected abstract void ExecuteBuyStrategy(MyTradeItem lastTrade);
        protected abstract void ExecuteSellStrategy(MyTradeItem lastTrade);
        
        
        
    }
    */
    /*
    public class MarginSimpleStrategy : TradingStrategyBase1
    {
        public MarginSimpleStrategy(ITradingToolsService tradingTools, MarketToolsDataAdapter marketDataAdapter, TradingToolsDataAdapter tradingToolsDataAdapter) : base(tradingTools, marketDataAdapter, tradingToolsDataAdapter)
        {
        }

        public override void Execute()
        {
            var lastPrice = MarketDataAdapter.GetLastPrice(Settings.Market.ToString());
            //TradingToolsDataAdapter.GetLastTrade()
        }

        protected override void ExecuteBuyStrategy(MyTradeItem lastTrade)
        {
            throw new NotImplementedException();
        }

        protected override void ExecuteSellStrategy(MyTradeItem lastTrade)
        {
            throw new NotImplementedException();
        }

      
    }*/

}