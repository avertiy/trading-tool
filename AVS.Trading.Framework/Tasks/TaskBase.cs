using System;
using System.Collections.Generic;
using AVS.CoreLib.Services.Logging.LogWriters;
using AVS.CoreLib.Services.Tasks.AppTasks;
using AVS.CoreLib.Utils;
using AVS.Trading.Core;
using AVS.Trading.Core.Domain;
using AVS.Trading.Core.Formatters;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Framework.Utils;

namespace AVS.Trading.Framework.Tasks
{
    public abstract class TaskBase : ParameterizedTask<TradingAppConfig, TaskParameters>
    {
        protected readonly ExchangeDirectory ExchangeDirectory;
        protected internal readonly IWorkContext WorkContext;

        protected TaskBase(TradingAppConfig config, IWorkContext workContext, ExchangeDirectory exchangeDirectory) : base(config)
        {
            WorkContext = workContext;
            ExchangeDirectory = exchangeDirectory;
        }

        /// <summary>
        /// pairs provided by parameter -pairs 
        /// possible values: all and pairs
        /// e.g. args ="-pair BTC_MAID" args ="-pairs BTC_MAID,BTC_LTC"
        /// </summary>
        protected internal string[] GetPairs(ExchangeClient x)
        {
            if (string.IsNullOrEmpty(Parameters.Pairs))
                throw new Exception("TaskParameters pairs arg is required");

            if (Parameters.Pairs == "all")
                return x.Pairs.GetAllPairs().ToArray();
            if (Parameters.Pairs == "x" || Parameters.Pairs == "target")
            {
                return AppConfig.Exchanges[x.Exchange].Pairs;
            }
            return Parameters.Pairs.Split(',');
        }
        
        protected internal TaskParameters GetParameters()
        {
            return Parameters;
        }

        protected internal IEnumerable<ExchangePair> IteratePairs(ExchangeClient x)
        {
            var pairs = GetPairs(x);
            if (pairs == null || pairs.Length == 0)
            {
                throw new ApplicationException($"No pairs to execute task {this.GetType().Name} over");
            }

            foreach (string pair in pairs)
            {
                if (x.Pairs.Contains(pair))
                    yield return new ExchangePair(pair, x.Exchange, x.Pairs.IsBaseCurrencyFirst);
            }
        }

        //


        protected internal IEnumerable<ExchangeClient> IterateExchanges()
        {
            if (string.IsNullOrEmpty(Parameters.Exchange))
            {
                foreach (ExchangeClient client in ExchangeDirectory.GetAllClients())
                {
                    yield return client;
                }
            }
            else
            {
                var client = ExchangeDirectory.GetClient(Parameters.Exchange);
                yield return client;
            }
        }

        public override void BeforeExecute(TaskLogWriter log)
        {
            log.FormatProvider = TradingFormatter.FormatProvider;
            //log.WriteSystemDetails(this.Config.ToString());
        }

        protected bool ShouldPrint(string pair)
        {
            return Parameters.Print == null || Parameters.Print.Contains(pair);
        }

        protected void ForEachAccount(string exchange, Action action)
        {
            if (!string.IsNullOrEmpty(Parameters.Account))
            {
                var apiKey = this.AppConfig.Exchanges.GetApiKey(exchange, Parameters.Account);
                this.WorkContext.SwitchAccount(apiKey);
                action();
            }
            else
            {
                var keys = this.AppConfig.Exchanges.GetApiKeys(exchange);
                foreach (var apiKey in keys)
                {
                    this.WorkContext.SwitchAccount(apiKey);
                    action();
                }
            }
        }

        
    }

    public static class TaskBaseExtensions
    {
        public static void ForEachExchange(this TaskBase task, Action<ExchangeClient> action)
        {
            foreach (var x in task.IterateExchanges())
            {
                task.WorkContext.Exchange = x.Exchange;
                action(x);
            }
        }

        public static void ForEachPair(this TaskBase task, Action<ExchangePair> action)
        {
            task.ForEachExchange(client =>
            {
                foreach (ExchangePair pair in task.IteratePairs(client))
                {
                    task.WorkContext.Exchange = client.Exchange;
                    action(pair);
                }
            });
        }

        public static void ForEachPair(this TaskBase task, Action<ExchangeClient, ExchangePair> action)
        {
            task.ForEachExchange(client =>
            {
                foreach (ExchangePair pair in task.IteratePairs(client))
                {
                    action(client, pair);
                }
            });
        }
    }

}