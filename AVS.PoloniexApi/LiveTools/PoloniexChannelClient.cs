using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AVS.CoreLib._System.Net;
using AVS.CoreLib._System.Net.WebSockets;
using AVS.PoloniexApi.General;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Interfaces.MarketTools;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace AVS.PoloniexApi.LiveTools
{
    public class PoloniexChannelClient : IDisposable
    {
        private readonly WSChannelClient _client;
        private Dictionary<TickerSymbol, PriceAggregatedBook> Books { get; set; }

        public PriceAggregatedBook this[PairString pair]
        {
            get
            {
                var symbol = GetTickerSymbol(pair.Value);
                return Books[symbol];
            }
        }

        public PoloniexChannelClient()
        {
            _client = new WSChannelClient(PoloniexConstants.WssApiUrl);
            _client.ConnectionClosed += OnSocketConnectionClosed;
            _client.ConnectionError += OnSocketConnectionError;
            _client.MessageArrived += OnMessageArrived;
            Books = new Dictionary<TickerSymbol, PriceAggregatedBook>();
        }
        public Task ConnectAsync()
        {
            return _client.ConnectAsync();
        }

        public Task SubscribeOnAsync(PairString pair)
        {
            try
            {
                var symbol = GetTickerSymbol(pair.Value);
                var cmd = new PublicChannelCommand() {Channel = (int) symbol, Command = CommandType.Subscribe};
                var book = new PriceAggregatedBook();
                Books.Add(symbol, book);
                return _client.SendAsync(cmd);
            }
            catch (Exception ex)
            {
                throw new Exception($"SubscribeOnAsync({pair}) failed", ex);
            }
        }

        public void Dispose()
        {
            _client.Dispose();
        }

        private void OnMessageArrived(string message)
        {
            if (message == "[1010]")
            {
                //connection is ok
                return;
            }

            using (var rdr = new JsonTextReader(new StringReader(message)))
            {
                JToken token = JToken.Load(rdr);
                if (token is JArray jArray)
                {
                    var channel = jArray[0].Value<int>();
                    switch (channel)
                    {
                        case 1002:
                            break;
                        default:
                        {
                                //ticker symbol
                            var symbol = (TickerSymbol)channel;
                            if(!Books.ContainsKey(symbol))
                                throw new Exception($"Unexpected symbol {symbol}");

                            Books[symbol].ParseMessage(jArray);
                            break;
                        }
                    }
                }

            }
        }

        private void OnSocketConnectionError(Exception obj)
        {
            throw new NotImplementedException("OnSocketConnectionError");
        }

        private void OnSocketConnectionClosed()
        {
            //reconnect
            var task = _client.ConnectAsync();
        }

        private static TickerSymbol GetTickerSymbol(string pair)
        {
            return (TickerSymbol)Enum.Parse(typeof(TickerSymbol), pair);
        }
    }


    interface IBackgroundService
    {
        //exchange pair because workcontext does not work behind foreground thread
        //depth is % from the spread
        Task<Response<IPublicOrderBook>> GetOrderBookAsync(ExchangePair pair, int depth=60);
    }


    class BackgroundServiceImpl : IBackgroundService
    {
        //private IStorage<IPublicOrderBook> _storage;
        private IChannelService _channelService;
        public async Task<Response<IPublicOrderBook>> GetOrderBookAsync(ExchangePair pair, int depth = 60)
        {
            var request = new OrderBookRequest() { Depth = depth, Pair = pair };
            var response = await _channelService.SendRequestAsync<IPublicOrderBook>(request);
            return response;
        }
    }

    interface IChannelService
    {
        Task<Response<T>> SendRequestAsync<T>(IRequest cmd);
    }

    interface IRequest
    {
        ExchangePair Pair { get; }
    }

    class OrderBookRequest: IRequest
    {
        public ExchangePair Pair { get; set; }
        public int Depth { get; set; }
    }

    interface IStorage<T>
    {
        bool TryGetValue(ExchangePair pair, out T value);
        void Put(ExchangePair pair, T obj);
    }

    class TreadSafeStorage<T>: IStorage<T>
    {
        private ConcurrentDictionary<ExchangePair, T> Data;

        public bool TryGetValue(ExchangePair pair, out T value)
        {
            return Data.TryGetValue(pair, out value);
        }
        public void Put(ExchangePair pair, T obj)
        {
            Data[pair] = obj;
        }
    }

    
}