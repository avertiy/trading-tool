using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Xml;
using AVS.CoreLib.Infrastructure.Config;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Interfaces;

namespace AVS.Trading.Framework.Infrastructure
{
    public class TradingAppConfig: AppConfig
    {
        public int OpenOrdersDepth { get; protected set; }
        public TradingNode Trading { get; protected set; }
        public ExchangeCollection Exchanges { get; protected set; }
       
        protected override void Initialize(object configContext, XmlNode section)
        {
            try
            {
                base.Initialize(configContext, section);

                Exchanges = new ExchangeCollection(section);
                
                XmlNode loadDataNode = section.SelectSingleNode("LoadData");
                if (loadDataNode != null)
                {
                    XmlNode openOrdersNode = loadDataNode.SelectSingleNode("OpenOrders");
                    OpenOrdersDepth = GetInt(openOrdersNode, "depth");
                }

                //XmlNode marketsNode = section.SelectSingleNode("Markets");
                //InitializeMarkets(marketsNode);

                Trading = new TradingNode(section.SelectSingleNode("Trading"));
            }
            catch (Exception ex)
            {
                throw new ConfigurationErrorsException("PoloniexAppConfig unable to initialize config",ex);
            }
        }

        public class TradingNode : XmlConfigNodeBase
        {
            public IList<StrategyNode> TradingStrategies { get; protected internal set; }

            public TradingNode(XmlNode node)
            {
                XmlNodeList nodes = node?.SelectNodes("Strategy");
                if(nodes == null)
                    return;
                TradingStrategies = new List<StrategyNode>(nodes.Count);
                foreach (XmlNode strategyNode in nodes)
                {
                    TradingStrategies.Add(new StrategyNode(strategyNode));
                }
                
            }

            public StrategyNode this[string strategyType]
            {
                get
                {
                    return TradingStrategies?.FirstOrDefault(s => s.Type == strategyType);
                }
            }

            public class StrategyNode : XmlConfigNodeBase
            {
                public string Type { get; protected internal set; }
                public string Market { get; protected internal set; }
                public bool Enabled { get; protected internal set; }

                public double? OrderAmountLimit { get; protected internal set; }
                public double? OrderTotalLimit { get; protected internal set; }
                public double? MarginThreshold { get; protected internal set; }

                public StrategyNode(XmlNode node)
                {
                    Type = this.GetString(node, "type");
                    Market = this.GetString(node, "market");
                    Enabled = this.GetBool(node, "enabled");
                    OrderAmountLimit = this.GetDouble(node, "order-amount-limit");
                    OrderTotalLimit = this.GetDouble(node, "order-total-limit");
                    MarginThreshold = this.GetDouble(node, "margin-threshold");
                }
            }

            
        }
    }

    public class ExchangeNode: XmlConfigNodeBase
    {
        public string Name { get; protected set; }
        public Collection<ApiKey> Keys { get; protected set; }
        public string[] Pairs { get; protected set; }
        public string[] WatchPairs { get; protected set; }

        

        public ExchangeNode(XmlNode node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));
            if(node.Name != "Exchange")
                throw new ConfigurationErrorsException($"Exchange node is expected", node);
            Name = this.GetString(node, "name");

            Keys = new Collection<ApiKey>();

            foreach (XmlNode child in node.ChildNodes)
            {
                if (child.Name == "ApiKey")
                {
                    var publicKey = this.GetString(child, "publicKey");
                    var privateKey = this.GetString(child, "privateKey");

                    if (string.IsNullOrEmpty(publicKey))
                        throw new ConfigurationErrorsException($"PublicKey is missing",child);
                    if (string.IsNullOrEmpty(privateKey))
                        throw new ConfigurationErrorsException($"PrivateKey is missing", child);
                    
                    var key = new ApiKey
                    {
                        Name = GetString(child, "account"),
                        PublicKey = GetString(child, "publicKey"),
                        PrivateKey = GetString(child, "privateKey")
                    };
                    Keys.Add(key);
                }
                else if (child.Name == "Pairs")
                {
                    InitializePairs(child);
                }
            }

            if (Keys.Count == 0)
                throw new ConfigurationErrorsException($"ApiKey is missing for exchange {Name}", node);
        }

        public ApiKey GetKey(string name)
        {
            var key = Keys.FirstOrDefault(k => k.Name == name);
            return key;
        }

        private void InitializePairs(XmlNode node)
        {
            if (node == null)
                return;

            var isBaseCurrencyFirst = GetBool(node, "baseCurrencyFirst");
            var baseCurrencies = GetString(node, "baseCurrencies");
            var quoteCurrencies = GetString(node, "quoteCurrencies");
            WatchPairs = GetString(node, "watch").Split(new[] {","}, StringSplitOptions.RemoveEmptyEntries);

            if (!string.IsNullOrEmpty(baseCurrencies) && !string.IsNullOrEmpty(quoteCurrencies))
                Pairs = DefaultPairProvider.CreateCombinations(baseCurrencies, quoteCurrencies, isBaseCurrencyFirst);
        }
    }

    public class ExchangeCollection : Collection<ExchangeNode>
    {
        public ExchangeCollection(XmlNode section)
        {
            if(section == null)
                throw new ArgumentNullException(nameof(section));

            var node = section["Exchanges"];
            if (node == null)
                throw new ConfigurationErrorsException("There must be Exchanges node", section);

            foreach (XmlNode child in node.ChildNodes)
            {
                var item = new ExchangeNode(child);
                Add(item);
            }

            if (Count == 0)
                throw new ConfigurationErrorsException($"There must be at least one Exchange", node);
        }

        public ExchangeNode GetExchange(string exchange)
        {
            return this.FirstOrDefault(k => k.Name == exchange);
        }

        public ApiKey GetApiKey(string exchange, string name)
        {
            return GetExchange(exchange)?.GetKey(name);
        }

        public ApiKey[] GetApiKeys(string exchange)
        {
            var ex = GetExchange(exchange);
            if(ex == null)
                throw new ArgumentException($"Invalid exchange argument: {exchange}");
            return ex.Keys.ToArray();
        }

        public ApiKey GetPrimaryApiKey(string exchange)
        {
            return GetExchange(exchange)?.Keys.FirstOrDefault();
        }

        public ExchangeNode this[string name] => GetExchange(name);
    }
}
