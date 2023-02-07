using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AVS.CoreLib.ConsoleTools.Utils;
using AVS.CoreLib.Utils;
using AVS.PoloniexApi.General;
using AVS.PoloniexApi.LiveTools;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.OrderBookWatch
{
    public class WatchOrderBookService
    {
        public static int PrintCount = 150;
        public static int UpdateBookIndex = 20;
        public static int RefreshScreenMinInterval = 30;
        public static DateTime LastScreenUpdate;
        public static Dictionary<string, double> Pairs { get; private set; } = new Dictionary<string, double>();

        public static string TargetPair { get; private set; }
        private readonly PoloniexPairProvider _pairProvider = new PoloniexPairProvider();
        readonly PoloniexChannelClient _client;

        public WatchOrderBookService(PoloniexChannelClient client)
        {
            _client = client;
        }

        public static bool SetWatchTarget(PairString pair)
        {
            if (Pairs.Keys.Any(k=>k == pair.Value))
            {
                TargetPair = pair.Value;
                //_client[TargetPair].Print(Pairs[TargetPair], PrintCount);
                return true;
            }

            return false;
        }

        public async Task SubscribeOn(string market, double minOrderAmount)
        {
            if (_pairProvider.TryGetPairByMarket(market, out string pair))
            {
                if(Pairs.ContainsKey(pair))
                    return;
                TargetPair = pair;
                await _client.ConnectAsync();
                await _client.SubscribeOnAsync(pair);
                _client[pair].Ready += OnBookReady;
                _client[pair].Trade += OnBookTradeUpdate;
                _client[pair].SellsUpdate += SellsUpdate;
                _client[pair].BuysUpdate += BuysUpdate;
                Pairs.Add(pair, minOrderAmount);
            }
        }
        
        private void OnBookReady(PriceAggregatedBook book)
        {
            if(TargetPair != book.Pair)
                return;
            book.Shrink(1.6);
            using (var locker = ConsoleLocker.Create())
            {
                Console.WriteLine("Initial book");
                Print(book);
                LastScreenUpdate = DateTime.Now;
            }
        }

        private void BuysUpdate(OrderUpdateEventArgs obj)
        {
            if (TargetPair != obj.Book.Pair)
                return;
            if (obj.Book.PeekIndex(obj.Price) > UpdateBookIndex)
                return;
            if ((DateTime.Now - LastScreenUpdate).TotalSeconds < RefreshScreenMinInterval)
                return;
            using (var locker = ConsoleLocker.Create())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Buys update");
                Console.WriteLine(
                    $"[{obj.Amount.FormatAsAmount()} x {obj.Price.FormatAsPrice()} => {obj.OrderAmount.FormatAsAmount()}");
                Console.ForegroundColor = ConsoleColor.White;
                Print(obj.Book);
                LastScreenUpdate = DateTime.Now;
            }
        }

        private void SellsUpdate(OrderUpdateEventArgs obj)
        {
            if (TargetPair != obj.Book.Pair)
                return;
            if (obj.Book.PeekIndex(obj.Price) > UpdateBookIndex)
                return;
            if ((DateTime.Now - LastScreenUpdate).TotalSeconds < RefreshScreenMinInterval)
                return;
            using (var locker = ConsoleLocker.Create())
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine("Sells update");
                Console.WriteLine(
                    $"[{obj.Amount.FormatAsAmount()} x {obj.Price.FormatAsPrice()} => {obj.OrderAmount.FormatAsAmount()}");
                Console.ForegroundColor = ConsoleColor.White;

                Print(obj.Book);
                LastScreenUpdate = DateTime.Now;
            }
        }

        private void OnBookTradeUpdate(TradeEventArgs obj)
        {
            if (TargetPair != obj.Book.Pair)
                return;
            if (obj.Book.PeekIndex(obj.Price) > UpdateBookIndex)
                return;
            if ((DateTime.Now - LastScreenUpdate).TotalSeconds < RefreshScreenMinInterval)
                return;
            using (var locker = ConsoleLocker.Create())
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Trade update");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(
                    $"{obj.Price.FormatAsPrice()} {obj.Amount.FormatAsAmount()} - {obj.TradeAmount.FormatAsAmount()}");

                Print(obj.Book);
                LastScreenUpdate = DateTime.Now;
            }
        }

        private void Print(PriceAggregatedBook book)
        {
            book.Print(Pairs[TargetPair], PrintCount);
        }

        public void PrintPairs()
        {
            ConsoleOut.Print("Watching pairs: " +string.Join(", ", Pairs.Keys), ConsoleColor.DarkGreen);
        }
    }
}