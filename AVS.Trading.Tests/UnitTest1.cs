using System;
using System.Collections;
using AVS.Trading.Core;
using AVS.Trading.Pipeline.Models;
using AVS.Trading.Pipeline.TradingAlgorithms.Context;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AVS.Trading.Tests
{
    [TestClass]
    public class TradingAlgorithmSetupTest
    {
        protected CurrencyPair Pair = new CurrencyPair("BTC","LTC");
        protected double MarketPrice = 0.00811;
       // protected TradingAlgorithm Algorithm;
        protected AlgorithmContext Context;
        protected TradeSessionModel Setup;

        [TestInitialize]
        public void TestInit()
        {
            var range = new PriceRange(0.0078,0.008);
            var buyRange = new PriceRange(0.0078,0.0079);
            var sellRange = new PriceRange(0.0079, 0.008);

            var setup = new TradeSessionModel()
            {
                Pair = Pair.ToString(),
                AmountQuote = 3,
                AvailableAmountQuote = 6,
                Range = new TradingRange(range, buyRange, sellRange)
            };
            
            Context = new AlgorithmContext("Exmo",Pair);
            //Context.Initialize(setup);
            //Algorithm = new TradingAlgorithm() { Context = Context };
        }

        [TestMethod]
        public void TestAlgorithmContextSetup()
        {
            Assert.AreEqual(Context.BalanceSheet[Pair.QuoteCurrency].Balance, 6.0); 
            Assert.AreEqual(Context.BalanceSheet[Pair.BaseCurrency].Balance, 0.0); 
            Assert.AreEqual(Context.Positions.Type, Core.Enums.PositionType.None); 
        }

      /*  [TestMethod]
        public void TestBalanceSheetInitialization()
        {
            BalanceSheet sheet = Algorithm.BalanceSheet;
            Assert.AreEqual(sheet.Pair, Pair);

            var balanceLtc = sheet["LTC"];
            Assert.AreEqual(balanceLtc.Balance, 6.0);
            Assert.AreEqual(balanceLtc.Currency, "LTC");

            var balanceBtc = sheet["BTC"];
            Assert.AreEqual(balanceBtc.Balance, 0.0);
            Assert.AreEqual(balanceLtc.Currency, "BTC");
        }

        [TestMethod]
        public void TestMethod1()
        {
            var currencyPair = CurrencyPair.ParsePair(Pair);

            //Algorithm
            //1. CheckMarketPrice => 

            //MarketConditions:
            //


            Algorithm.Execute(MarketPrice);
            var sheet = Algorithm.BalanceSheet;

            //Start.AvailableAmountQuote-Start.AmountQuote
            Assert.AreEqual(sheet[currencyPair.QuoteCurrency].Balance, 3.0);

        }*/
    }
}
