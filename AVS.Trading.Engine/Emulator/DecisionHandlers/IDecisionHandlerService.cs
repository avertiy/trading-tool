using System;
using System.CodeDom;
using System.Linq;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Formatters;
using AVS.Trading.Core.Models;
using AVS.Trading.Data.Domain.TradingTools;
using AVS.Trading.Engine.Emulator.Context;

namespace AVS.Trading.Engine.Emulator.DecisionHandlers
{
    public interface IDecisionHandlerService
    {
        PostOrderResult PostOrder(string pair, OrderType type, double pricePerCoin, double amountQuote);
        CancelOrderResult CancelOrder(string pair, string orderId);
    }

    public class EmulatorDecisionHandlerService : IDecisionHandlerService
    {
        private int orderIdentity = 1;
        private readonly EmulatorDataProvider _dataProvider;

        public EmulatorDecisionHandlerService(EmulatorDataProvider dataProvider)
        {
            _dataProvider = dataProvider;
        }

        public PostOrderResult PostOrder(string pair, OrderType type, double pricePerCoin, double amountQuote)
        {
            var order = new OpenOrder()
            {
                Type = type,
                Account = TradingAccount.Exchange,
                AmountQuote = amountQuote,
                Price = pricePerCoin,
                AmountBase = (amountQuote * pricePerCoin).Normalize(),
                DateUtc = DateTime.Now,
                Pair = pair,
                OrderNumber = "00" + orderIdentity++,
                Id = (_dataProvider.OpenOrders.Count + 1),
                State = OrderState.Open
            };

            _dataProvider.OpenOrders.Add(order);
            var res = new PostOrderResult()
            {
                Success = true,
                OrderId = order.OrderNumber,
            };
            return res;
        }

        public CancelOrderResult CancelOrder(string pair, string orderId)
        {
            var order = _dataProvider.OpenOrders.FirstOrDefault(o => o.Pair == pair && o.OrderNumber == orderId);

            if (order != null)
            {
                order.State = OrderState.Canceled;
                _dataProvider.OpenOrders.Remove(order);
                _dataProvider.ArchivedOrders.Add(order);
            }

            return new CancelOrderResult()
            {
                Success = true,
                OrderId = orderId
            };
        }
    }

    public class BalanceSheetService
    {
        public BalanceSheet Balance { get; set; }

        public void OpenOrder(AlgorithmContext ctx, OpenOrder order)
        {
            var balance = ctx.State.Balance;
            var pair = ctx.Pair;
            if (order.Type == OrderType.Buy)
            {
                var comment = TradingFormatter.Format($"-{order.AmountBase} {pair:b} => placed buy order ID#{order.Id,-4} {order.AmountQuote} {pair:q}");
                balance[pair.BaseCurrency].Debit(order.AmountBase, comment);
            }
        }
    }

    
}