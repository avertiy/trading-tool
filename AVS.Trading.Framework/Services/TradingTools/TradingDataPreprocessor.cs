using System;
using System.Collections.Generic;
using System.Linq;
using AVS.CoreLib.Infrastructure;
using AVS.Trading.Core;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Interfaces.TradingTools;
using AVS.Trading.Data.Domain.TradingTools;

namespace AVS.Trading.Framework.Services.TradingTools
{
    public interface ITradingDataPreprocessor
    {
        List<TradeItem> PreprocessTrades(IEnumerable<ITrade> trades, string pair);
        List<TradeItem> PreprocessTrades(IDictionary<string, IList<ITrade>> alltrades);
        IDictionary<string, double> FilterOutZeroBalances(IDictionary<string,double> balances);
        Tuple<double, double> FilterOutBalances(IDictionary<string,double> balances, CurrencyPair pair);

        IList<OpenOrder> PreprocessOrders(IList<ILimitOrder> orders, Action<OpenOrder> action);

    }

    public class TradingDataPreprocessor : ITradingDataPreprocessor
    {
        public List<TradeItem> PreprocessTrades(IEnumerable<ITrade> trades, string pair)
        {
            var items = new List<TradeItem>();

            if (trades != null)
            {
                foreach (ITrade trade in trades)
                {
                    var item = new TradeItem();
                    item = trade.Map(item);
                    item.Pair = pair;
                    item.OrderId = trade.OrderNumber;
                    item.TradeId = trade.IdTrade;
                    item.TotalFee = trade.TotalFee;
                    items.Add(item);
                }
            }

            return items;
        }

        public List<TradeItem> PreprocessTrades(IDictionary<string, IList<ITrade>> alltrades)
        {
            var items = new List<TradeItem>();
            if (alltrades != null)
            {
                foreach (var kp in alltrades)
                {
                    foreach (ITrade trade in kp.Value)
                    {
                        var item = new TradeItem();
                        item = trade.Map(item);
                        item.Pair = kp.Key;
                        item.OrderId = trade.OrderNumber;
                        item.TradeId = trade.IdTrade;
                        item.TotalFee = trade.TotalFee;
                        items.Add(item);
                    }
                }
            }

            return items;
        }

        public List<OpenOrder> PreprocessOpenOrders(IList<ILimitOrder> orders, PairString pair)
        {
            var myOrders = new List<OpenOrder>();
            if (orders != null)
            {
                foreach (ILimitOrder order in orders)
                {
                    var myOrder = new OpenOrder()
                    {
                        Pair = pair.Value,
                        OrderNumber = order.IdOrder.ToString(),
                        Account = order.Account,
                        AmountQuote = order.AmountQuote,
                        InitialAmount = order.AmountQuote,
                        AmountBase = order.AmountBase,
                        Price = order.Price,
                        DateUtc = null,
                        Comment = null,
                        Condition = OrderCondition.None,
                        State = OrderState.Open,
                        StopLoss = null,
                        CreatedOnUtc = DateTime.Today,
                        Type = order.Type,
                        TakeProfit = null,
                        Exchange = order.Exchange
                    };
                    myOrders.Add(myOrder);
                }
            }

            return myOrders;
        }

        public IDictionary<string, double> FilterOutZeroBalances(IDictionary<string, double> balances)
        {
            if(balances == null)
                throw new ArgumentNullException(nameof(balances));
            return balances.Where(d => d.Value > 0).ToDictionary(k => k.Key, v => v.Value);
        }

        public Tuple<double, double> FilterOutBalances(IDictionary<string, double> balances, CurrencyPair pair)
        {
            if (balances == null)
                return null;
            if (pair == null || pair == CurrencyPair.Any)
                return null;
            var res = new Tuple<double, double>(balances[pair.QuoteCurrency], balances[pair.BaseCurrency]);
            return res;
        }

        public IList<OpenOrder> PreprocessOrders(IList<ILimitOrder> orders, Action<OpenOrder> action)
        {
            var myOrders = new List<OpenOrder>();

            foreach (ILimitOrder limitOrder in orders)
            {
                var order = new OpenOrder()
                {
                    OrderNumber = limitOrder.IdOrder.ToString(),
                    Account = limitOrder.Account,
                    AmountQuote = limitOrder.AmountQuote,
                    InitialAmount = limitOrder.AmountQuote,
                    AmountBase = limitOrder.AmountBase,
                    Price = limitOrder.Price,
                    DateUtc = null,
                    Comment = null,
                    Condition = OrderCondition.None,
                    State = OrderState.Open,
                    StopLoss = null,
                    CreatedOnUtc = DateTime.Today,
                    Type = limitOrder.Type,
                    TakeProfit = null,
                    Exchange = limitOrder.Exchange
                };

                action?.Invoke(order);

                myOrders.Add(order);
            }

            return myOrders;
        }
    }
}