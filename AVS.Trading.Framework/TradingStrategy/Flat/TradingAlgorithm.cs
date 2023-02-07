using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AVS.Poloniex.Framework.Adapters;
using AVS.Trading.Core.Enums;
using AVS.Trading.Data.Domain.TradingTools;

namespace AVS.Poloniex.Framework.TradingStrategy.Flat
{
   
    

    


    public class AlgorithmHelper
    {
        public TradeItem LastTrade { get; set; }
        public OpenOrder LastOpenOrder { get; set; }

        public void RefreshOrders()
        {

        }
    }


    
    



/*
    public class SmartStopAlgo
    {
        private MarketToolsDataAdapter _marketDataAdapter;
        private AlgorithmSetup _setup;
        public void Execute()
        {
            double marketPrice = 0.0083;


            var condition = _setup.GetCondition(marketPrice);
            condition.Execute(marketPrice);
        }

    }*/
    
    

    
    /*

    

    public interface IStopOrderCondition
    {
        bool Match(double price);
    }

    public interface IContinueTradingCondition
    {
        bool Match(double price);
    }

    public class ContinueTradingCondition: IContinueTradingCondition
    {
        public PriceRange Range { get; set; }
        public double MinMargin { get; set; }

        public virtual bool Match(double price)
        {
            if (Range.Match(price))
            {

            }
            return false;
        }
    }


    public abstract class StopOrderCondition : IStopOrderCondition
    {
        public double Price { get; set; }
        public abstract bool Match(double price);
    }

    public class PumpStopOrderCondition : StopOrderCondition
    {
        public override bool Match(double price)
        {
            if (price > Price)
            {
                //check market for pump situation
                //get market trades, make up buy/sell volumes compare to avg volume in prev. time 5M frames
                //if volumes are bigger than avg than we have a pump 
                return true;
            }
            return false;
        }
    }*/
}
