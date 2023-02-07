using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AVS.Trading.Engine
{

    //Algorithm version 1.0 - проблема узко заточеный и перегруженный вспомогательной хренью 
    /*
    public abstract class BaseAlgorithm : IAlgorithm
    {
        protected MarketContextBuilder MarketContextBuilder;
        protected TradingContextBuilder TradingContextBuilder;
        protected AlgorithmContext Context;
        protected BaseAlgorithm(MarketContextBuilder marketContextBuilder, TradingContextBuilder tradingContextBuilder)
        {
            MarketContextBuilder = marketContextBuilder;
            TradingContextBuilder = tradingContextBuilder;
        }

        public void Execute(AlgorithmContext ctx)
        {
            Context = ctx;
            FillInContext(Context);
            Execute();
        }

        protected abstract void Execute();

        protected virtual void FillInContext(AlgorithmContext ctx)
        {
            MarketContextBuilder.Build(ctx);
            TradingContextBuilder.Build(ctx);
        }

        protected ITradingToolsService TradingToolsService;

        protected void DoSell(double amount, double price)
        {
            var pair = Context.Pair.ToString();
            var response = TradingToolsService.PostOrder(pair, OrderType.Sell, price, amount);
            if (response.Success && !string.IsNullOrEmpty(response.Data.IdOrder))
            {
                //do sell
            }
        }

    }


    public abstract class BaseMarketPriceAlgorithm : BaseAlgorithm
    {
        protected BaseMarketPriceAlgorithm(MarketContextBuilder marketContextBuilder, TradingContextBuilder tradingContextBuilder) 
            : base(marketContextBuilder, tradingContextBuilder)
        {
        }

        protected override void Execute()
        {
            MatchPrice(Context.MarketContext.MarketData.PriceLast);
        }

        protected abstract void PriceAboveTradingRange();
        protected abstract void PriceBelowTradingRange();
        protected abstract void PriceOutOfTradingRange();
        protected abstract void PriceMatchesBuyRange();
        protected abstract void PriceMatchesSellRange();
        protected abstract void PriceMatchesTradingRange();

        protected virtual void MatchPrice(double price)
        {
            var range = Context.Range;
            if (!range.Match(price))
            {
                if (Context.Positions.Any())
                {
                    if (price > range.PriceMax)
                        PriceAboveTradingRange();
                    else if (price < range.PriceMin)
                        PriceBelowTradingRange();
                }
                else
                {
                    PriceOutOfTradingRange();
                }
            }
            else
            {
                if (range.BuyRange.Match(price))
                {
                    PriceMatchesBuyRange();
                }
                else if (range.SellRange.Match(price))
                {
                    PriceMatchesSellRange();
                }
                else
                {
                    PriceMatchesTradingRange();
                }
            }
        }
        
    }
    
    public class MarketPriceAlgorithm : BaseMarketPriceAlgorithm
    {
        //let's say algorithm is run every 5 mins or even every 1 min.
        //so the market price might not be chaged a lot since the last run in 99% of cases
        //if we 


        public MarketPriceAlgorithm(MarketContextBuilder marketContextBuilder, TradingContextBuilder tradingContextBuilder) 
            : base(marketContextBuilder, tradingContextBuilder)
        {
        }

        protected override void PriceAboveTradingRange()
        {
            var posType = Context.Positions.Type;
            if(posType == PositionType.None)
                return;
            var positionMap = Context.Positions;
            if (posType == PositionType.Long)
            {
                //fix profit => sell
                //let's say strategy 50%
                var amountToSell = positionMap.Amount * 0.5;

                //if (positionMap.Shorts.LastTradeTime == null)
                    //DoSell(amountToSell);

            }
            else
            {
                //stop-loss => buy
            }
        }

        protected override void PriceBelowTradingRange()
        {
            var posType = Context.Positions.Type;
            if (posType == PositionType.None)
                return;

            if (posType == PositionType.Short)
            {
                //fix profit => buy
            }
            else
            {
                //stop-loss => sell
            }
        }

        protected override void PriceOutOfTradingRange()
        {
            //do nothing
            //throw new System.NotImplementedException();
        }

        protected override void PriceMatchesBuyRange()
        {
            throw new System.NotImplementedException();
        }

        protected override void PriceMatchesSellRange()
        {
            throw new System.NotImplementedException();
        }

        protected override void PriceMatchesTradingRange()
        {
            throw new System.NotImplementedException();
        }
    }

    public enum ExecutionPriority
    {
        None = 0,
        ImmediateByMarket = 1,
        ImmediateByBestPrice = 2,
        Regular,
        Limit
    }
    */

    /*
   //matrix.GetDayRange() => [31.48;33.49]

   //matrix.GetHighs()=> [0.5h: 0.0033; 1h=>...; 4h;]
   //matrix.Month.H/L
   //matrix.Week[0].Day[1].
   //matrix.Day[0]

   //slice=> [M;W;D;4h;1h;0.5h;0.25h]
   //matrix=> [;;;;;;;=>[;;;]]

   public class Slice
   {
       private double[] Data => new double[8];

       public double Month => Data[7];
       public double Week => Data[6];
       public double Day => Data[5];
       public double H4 => Data[4];
       public double H1 => Data[3];
       public double M30 => Data[2];
       public double M15 => Data[1];
       public double M5 => Data[0];

       public static implicit operator double[](Slice foo)
       {
           return foo.Data;
       }

       public double this[int index]
       {
           get => Data[index];
           set => Data[index] = value;
       }
   }


   public class OhlcMatrix
   {
       private double[][] Data => new double[8][];

       public double[] this[int index]
       {
           get => Data[index];
           set => Data[index] = value;
       }
   }


   */
}
