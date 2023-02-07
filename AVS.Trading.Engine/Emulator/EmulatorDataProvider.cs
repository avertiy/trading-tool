using System;
using System.Collections.Generic;
using System.Linq;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Core.ResponseModels.MarketTools;
using AVS.Trading.Data.Domain.MarketTools;
using AVS.Trading.Data.Domain.TradingTools;
using AVS.Trading.Engine.Emulator.DecisionHandlers;
using AVS.Trading.Framework.Adapters;
using AVS.Trading.Framework.Services.MarketTools;

namespace AVS.Trading.Engine.Emulator
{
    public class EmulatorDataProvider
    {
        private int tradeIdIdentity = 1;
        private readonly IMarketToolsService _marketToolsService;
        public IDictionary<ICandlestick, IResult> Results { get; protected set; }
        public IList<ICandlestick> ChartData { get; protected set; }
        public IList<ICandlestick> ChartDataTestSet { get; protected set; }
        public IList<TradeItem> Trades { get; protected set; }
        public IList<OpenOrder> OpenOrders { get; protected set; }

        public IList<OpenOrder> ArchivedOrders { get; protected set; }

        public MarketData Ticker => CreateTicker(ChartData.First());

        public EmulatorDataProvider(IMarketToolsService marketToolsService)
        {
            _marketToolsService = marketToolsService;
        }

        public void Setup(Parameters parameters)
        {
            IList<ICandlestick> candles = LoadChartData(parameters);
            var count = candles.Count / 100 * (100 - parameters.TestSetShare);
            ChartData = candles.Take(count).ToList();
            ChartDataTestSet = candles.Skip(count).ToList();
            Trades = new List<TradeItem>();
            OpenOrders = new List<OpenOrder>();
            ArchivedOrders = new List<OpenOrder>();
            Results = new Dictionary<ICandlestick, IResult>(ChartDataTestSet.Count);
        }

        private IList<ICandlestick> LoadChartData(Parameters parameters)
        {
            ChartData data = _marketToolsService.LoadChartData(
                parameters.Pair.ToString(),
                parameters.MarketPeriod,
                parameters.Start, parameters.End);
            return data.Data;
        }

        private MarketData CreateTicker(ICandlestick candle)
        {
            var rand = new Random();
            var price = rand.GetRandomNumber(candle.Low, candle.High).Normalize();
            
            var data = new MarketData()
            {
                PriceLast = price,
                DateUtc = candle.Time,
                HighestBid = (price* rand.GetRandomNumber(0.99,0.999)).Normalize(),
                LowestAsk = (price * rand.GetRandomNumber(1.0, 1.01)).Normalize(),
            };
            return data;
        }


        public void ExecuteOrders(ICandlestick candle)
        {
            var makerFee = 0.08 / 100;

            foreach(var order in OpenOrders.Where(o => candle.Contains(o.Price)).ToArray())
            {
                order.State = OrderState.Executed;
                ArchivedOrders.Add(order);
                OpenOrders.Remove(order);
                
                var trade = new TradeItem()
                {
                    Price = order.Price,
                    AmountQuote = order.AmountQuote,
                    AmountBase = order.AmountBase,
                    Exchange = order.Exchange,
                    Type = order.Type == OrderType.Buy ? TradeType.Buy : TradeType.Sell,
                    Category = order.Account == TradingAccount.Exchange
                        ? TradeCategory.Exchange
                        : TradeCategory.MarginTrade,
                    DateUtc = candle.Time,
                    Fee = order.AmountQuote * makerFee,
                    TotalFee = order.AmountQuote * order.Price * makerFee,
                    OrderId = order.OrderNumber,
                    Pair = order.Pair,
                    TradeId = "00" + tradeIdIdentity++,
                    Id = tradeIdIdentity
                };

                Trades.Add(trade);
            }
        }
        

        public void SaveResult(ICandlestick candle, IResult result)
        {
            Results.Add(candle, result);
        }
    }

    public static class RandomExtensions
    {
        public static double GetRandomNumber(this Random r, double minNumber, double maxNumber)
        {
            return r.NextDouble() * (maxNumber - minNumber) + minNumber;
        }
    }
}