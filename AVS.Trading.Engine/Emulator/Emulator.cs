using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.Utils;
using AVS.Trading.Core;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Formatters;
using AVS.Trading.Core.Interfaces.MarketTools;
using AVS.Trading.Core.Models;
using AVS.Trading.Data.Domain.TradingTools;
using AVS.Trading.Engine.Emulator.Algorithms;
using AVS.Trading.Engine.Emulator.Context;
using AVS.Trading.Engine.Emulator.DecisionHandlers;
using AVS.Trading.Engine.Emulator.Services;
using AVS.Trading.Engine.Models;
using AVS.Trading.Framework;
using AVS.Trading.Pipeline.Models;

namespace AVS.Trading.Engine.Emulator
{
    public class TradingEmulator<T> where T:IAlgorithm
    {
        protected ConsoleWriter ConsoleWriter = new ConsoleWriter(new TradingFormatProvider());
        private readonly EmulatorDataProvider _dataProvider;
        private readonly TradingEngine _engine;
        private readonly BalanceSheetHelper _balanceSheetHelper;
        public TradingEmulator(EmulatorDataProvider dataProvider, TradingEngine engine, BalanceSheetHelper balanceSheetHelper)
        {
            _dataProvider = dataProvider;
            engine.Factory = new AlgorithmFactory<T>();
            _engine = engine;
            _balanceSheetHelper = balanceSheetHelper;
        }

        public void Run(Parameters parameters)
        {
            Console.WriteLine("Trade emulation");
            Console.WriteLine($"{parameters}");
            _dataProvider.Setup(parameters);
            parameters.Algorithm = typeof(T).Name;

            ConsoleWriter.WriteLine($"initial balance: {parameters.InitialBalance}");

            foreach (var candle in _dataProvider.ChartDataTestSet)
            {
                //add new candle to chart data
                _dataProvider.ChartData.Insert(0, candle);
                //execute open orders if any 
                _dataProvider.ExecuteOrders(candle);

                //run engine
                IResult result = _engine.Run(parameters);

                _dataProvider.SaveResult(candle, result);

                if(_dataProvider.Trades.Count >5)
                    break;
                //PrintResult(candle, result);
            }

            Console.WriteLine("TRADES: ");
            foreach (TradeItem trade in _dataProvider.Trades)
            {
                Print(trade);
            }
            //print results
            
            //foreach (KeyValuePair<ICandlestick, IResult> kp in _dataProvider.Results)
            //{
            //    PrintResult(kp.Key, kp.Value);
            //}

            ConsoleExt.SetDefaultColor();
            Console.WriteLine("RESULTS: ");
            BalanceSheet balanceSheet = _balanceSheetHelper.GetBalanceSheet(parameters);

            Console.WriteLine(balanceSheet.ToString());
            ConsoleExt.SetGrayColor();
            //Console.WriteLine("transactions: ");
            //Console.WriteLine(balanceSheet.GetTransactionsLog());
        }

        private void Print(TradeItem trade)
        {
            Console.WriteLine(trade.ToString());
        }


        public void PrintResult(ICandlestick candle, IResult result)
        {
            if(result is EmptyResult)
                return;
            ConsoleWriter.SetGrayColor();
            ConsoleWriter.WriteLine("{0:MM.dd hh:mm}: bar [{1:hl}] => {2}", candle.Time, candle, result);

            ConsoleWriter.SetDefaultColor();
            ConsoleWriter.WriteLine($"balance: {result.Balance}");
        }
    }

    public class Parameters
    {
        public string Exchange;
        public string Algorithm;
        public CurrencyPair Pair;
        public double Amount;
        public int CandlesCount =10;

        //emulator settings
        public DateTime Start;
        public DateTime End;
        public MarketPeriod MarketPeriod = MarketPeriod.M30;
        public int TestSetShare = 20;

        public BalanceSheet InitialBalance { get; set; }

        public override string ToString()
        {
            return $"{Algorithm} {Pair} date range: {Start:d} - {End:d} scale: {MarketPeriod}";
        }
    }

    public class BalanceSheetHelper
    {
        private readonly ITradeSessionService _tradeSessionService;

        public BalanceSheetHelper(ITradeSessionService tradeSessionService)
        {
            _tradeSessionService = tradeSessionService;
        }

        public BalanceSheet GetBalanceSheet(Parameters parameters)
        {
            TradeSession session = _tradeSessionService.GetSession(parameters.Exchange, parameters.Pair, parameters.Algorithm);
            if (session == null)
                return null;
            var balanceSheet = BalanceSheet.FromJson(session.BalanceSheetJson);
            return balanceSheet;
        }
    }
}
