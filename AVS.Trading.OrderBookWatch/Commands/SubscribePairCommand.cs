using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVS.CoreLib.ConsoleTools.Commands;
using AVS.CoreLib.ConsoleTools.Utils;
using AVS.PoloniexApi.General;

namespace AVS.Trading.OrderBookWatch.Commands
{
    public class SubscribePairCommand : ConsoleCommand
    {
        private readonly PoloniexPairProvider _pairProvider = new PoloniexPairProvider();
        public override void Execute(string input, IDictionary<string, string> args)
        {
            if (args.ContainsKey("count"))
                WatchOrderBookService.PrintCount = int.Parse(args["count"]);
            var pair = GetPair(args);
            ConsoleOut.PrintF($"{Name:DarkBlue}: subscribing on {pair:DarkGreen}");
            Program.Subscribe(pair, 0);
        }

        private string GetPair(IDictionary<string, string> args)
        {
            string input = null;
            if (args.ContainsKey("pair"))
            {
                input = args["pair"];
            }
            do
            {
                if (_pairProvider.TryGetPairByMarket(input, out string pair))
                {
                    return pair;
                }
                ConsoleOut.Print($"Invalid Poloniex pair: '{input}'", ConsoleColor.DarkRed);
                ConsoleOut.Print("Enter pair:", ConsoleColor.DarkYellow);
                input = Console.ReadLine();
            } while (true);
        }

        public override string Name => "subscribe pair";
        public override string Description => "subscribe on pair to watch it, parameters: -pair [required]";
        public override string[] Shortcuts => new[] { "/subscribe", "/s", "/w" };
    }

    public class PrintOptionsCommand : ConsoleCommand
    {
        private readonly PoloniexPairProvider _pairProvider = new PoloniexPairProvider();
        public override void Execute(string command, IDictionary<string, string> args)
        {
            if(args.ContainsKey("count"))
                WatchOrderBookService.PrintCount = int.Parse(args["count"]);
            if (args.ContainsKey("pair"))
            {
                if (_pairProvider.TryGetPairByMarket(args["pair"], out string pair))
                {
                    if (WatchOrderBookService.SetWatchTarget(pair))
                    {
                        ConsoleOut.Print("Watching pair: " + pair, ConsoleColor.DarkGreen);
                    }
                    else
                    {
                        Program.Subscribe(pair, 0);
                    }
                }
                else
                {
                    ConsoleOut.Print($"Pair {args["pair"]} is not supported by Poloniex pair provider", ConsoleColor.DarkRed);
                }
            }
                
        }

        public override string Name => "print options";
        public override string Description => "print options:\r\n -count - order book depth to be printed\r\n -pair - switch pair to be watched";
        public override string[] Shortcuts => new[] { "/options", "/o"};
    }
    
    public class SwitchPairCommand : ConsoleCommand
    {
        public override void Execute(string command, IDictionary<string, string> args)
        {
            var pairs = WatchOrderBookService.Pairs.Keys.ToList();
            var target = WatchOrderBookService.TargetPair;
            var ind = pairs.FindIndex(p => p == target);
            ind++;
            if (pairs.Count <= ind)
                ind = 0;
            Console.Clear();
            ConsoleOut.PrintF($"{Name:DarkBlue}: switched {target:DarkGray} to {pairs[ind]:DarkGreen}");
            WatchOrderBookService.SetWatchTarget(pairs[ind]);
            
        }

        public override ConsoleKey HotKey => ConsoleKey.Tab;
        public override string Name => "Switch Pair";
        public override string Description => "switch between watchable pairs.";
        public override string[] Shortcuts => new[] { "/switch" };
    }
    
}