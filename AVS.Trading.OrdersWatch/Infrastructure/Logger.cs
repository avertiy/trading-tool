using AVS.CoreLib.Infrastructure;
using AVS.CoreLib.Services.Logging;
using AVS.CoreLib.Services.Logging.Loggers;

namespace AVS.Trading.OrdersWatch.Infrastructure
{
    /// <summary>
    /// Represents a composite logger includes ConsoleLogger and DatabaseLogger
    /// </summary>
    public class Logger : CompositeLogger
    {
        public Logger()
        {
            AddLogger(new ConsoleLogger());
            AddLogger(new DatabaseLogger(EngineContext.Current.Resolve<ILogEntityService>()));
        }
    }

   
}