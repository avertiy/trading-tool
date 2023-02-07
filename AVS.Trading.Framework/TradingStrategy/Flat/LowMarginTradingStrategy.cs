using AVS.Trading.Framework.Adapters;
using AVS.Trading.Framework.Services.TradingTools;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Framework.TradingStrategy.Flat
{
    /*
    /// <summary>
    /// Низкомаржинальная торгоая стратегия (не путать Exchange/Margin trading)
    /// Ставим низкий коэффициент порога маржи для входа в сделку
    /// На основании предыдущего ордера прибавляем коэффициент маржи и если рыночная цена позвоялет входим в сделку
    /// </summary>
    public class LowMarginTradingStrategy : TradingStrategyBase1
    {
        public double MarginRatio = 1.01;//0.85%
        
        protected override void ExecuteBuyStrategy(MyTradeItem lastTrade)
        {
            var price = GetBuyPrice(lastTrade);
            double marketPrice = MarketDataAdapter.GetLastPrice(Market.ToString());
            if (marketPrice > price)
            {
                OrderNotPlaced($"Market price {marketPrice} > buyPrice {price}");
                return;
            }

            var amount = Settings.CheckOrderAmount(lastTrade.AmountQuote, price);

            PlaceOrder(OrderType.Buy, price, amount);
        }

        protected override void ExecuteSellStrategy(MyTradeItem lastTrade)
        {
            var price = GetSellPrice(lastTrade);
            double marketPrice = MarketDataAdapter.GetLastPrice(Market.ToString());
            if (marketPrice < price)
            {
                OrderNotPlaced($"Market price {marketPrice} < sellPrice {price}");
                return;
            }
            //when we bought thre 0.25% comission has been subtracted so the balance is AmountQuote *0.9975
            var quoteAmount = lastTrade.AmountQuote* (1-0.0025);
            var amount = CheckOrderAmount(Market,quoteAmount, price);
            PlaceOrder(OrderType.Sell, price, amount);
        }

        protected virtual double GetBuyPrice(MyTradeItem lastTrade)
        {
            return (lastTrade.Price / MarginRatio).RoundDown(Constants.PricePrecisionDigits);
        }

        protected virtual double GetSellPrice(MyTradeItem lastTrade)
        {
            return (lastTrade.Price * MarginRatio).RoundUp(Constants.PricePrecisionDigits);
        }
    }
    */
}
