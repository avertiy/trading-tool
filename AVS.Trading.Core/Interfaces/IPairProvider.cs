using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Enums;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Core.Interfaces
{
    /// <summary>
    /// Returns pairs* listed on exchange
    /// (*) means only pairs I'm considering for trading
    /// </summary>
    public interface IPairProvider
    {
        List<string> GetAllPairs();

        List<string> GetRecentPairs();

        PairString GetPair(string market);
        string[] GetPairs(string[] markets);
        
        bool TryGetPairByMarket(string market, out string pair);
        bool IsBaseCurrencyFirst { get; }
        
        string[] GetCoinsFor(AccountType type);

        bool Contains(string pair);
    }
    
    public abstract class DefaultPairProvider : IPairProvider
    {
        private List<string> _pairs;
        
        public List<string> Pairs
        {
            get
            {
                if(_pairs == null)
                    Initialize();
                return _pairs;
            }
            protected set => _pairs = value;
        }
        
        protected abstract void Initialize();

        public abstract string[] GetCoinsFor(AccountType type);

        public bool Contains(string pair)
        {
            return Pairs.Contains(pair);
        }

        public List<string> GetAllPairs()
        {
            if (_pairs == null)
                Initialize();
            return _pairs;
        }

        public virtual List<string> GetRecentPairs()
        {
            return GetAllPairs();
        }

        public virtual bool IsBaseCurrencyFirst => true;
        public virtual bool SupportAllKeyword => false;

        public string[] GetPairs(string[] markets)
        {
            var result = new string[markets.Length];
            for (var i = 0; i < markets.Length; i++)
            {
                var market = markets[i];
                result[i] = GetPair(market);
            }

            return result;
        }
        
        /// <summary>
        /// If market string value represents one of registered pairs, the pair will be returned, otherwise ArgumentException will be thrown
        /// </summary>
        /// <param name="market">e.g. BTS/BTC or  BTC/UAH</param>
        /// <returns>if recognized pair, returns pair e.g. BTC_BTS or BTC_UAH</returns>
        //[DebuggerStepThrough]
        public PairString GetPair(string market)
        {
            if(string.IsNullOrEmpty(market))
                throw new ArgumentNullException(nameof(market));
            //market might be BTS/BTC but pair is BTC_BTS on poloniex, quote currency is BTS
            //market might be BTC/UAH and pair is BTC_UAH on Exmo, quoite currency is BTC

            var pair = market.Replace("/", "_");

            //e.g. BTC_DOGE on exmo is DOGE_BTC
            if (Pairs.Contains(pair))
                return pair;

            string swap = pair.Swap('_');

            if(Pairs.Contains(swap))
                return swap;

            if (market == CurrencyPair.All && SupportAllKeyword)
                return market;

            throw new ArgumentException($"The {market} market is not recognized. [Please check whether the market has been registered by {this.GetType().Name}]");
        }

        public bool TryGetPairByMarket(string market, out string pair)
        {
            pair = null;
            if (string.IsNullOrEmpty(market))
                return false;

            //market might be BTS/BTC but pair is BTC_BTS on poloniex, quote currency is BTS
            //market might be BTC/UAH and pair is BTC_UAH on Exmo, quoite currency is BTC

            pair = market.ToUpper().Replace("/", "_");

            //e.g. BTC_DOGE on exmo is DOGE_BTC
            if (Pairs.Contains(pair))
                return true;

            pair = pair.Swap('_');

            if (Pairs.Contains(pair))
                return true;

            pair = null;
            return false;
        }



        public static string[] CreateCombinations(string baseCurrencies, string quoteCurrencies, bool isBaseCurrencyFirst=true)
        {
            var pairs = new List<string>();
            foreach (var baseCur in baseCurrencies.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).OrderBy(c => c))
            {
                foreach (var quoteCur in quoteCurrencies.Split(new[] { "," }, StringSplitOptions.RemoveEmptyEntries).OrderBy(c=>c))
                {
                    if(isBaseCurrencyFirst)
                        pairs.Add(baseCur + "_" + quoteCur);
                    else
                        pairs.Add(quoteCur + "_" + baseCur);
                }
            }
            return pairs.ToArray();
        }
    }

    }