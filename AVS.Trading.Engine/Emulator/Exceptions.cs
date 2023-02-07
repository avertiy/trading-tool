using System;

namespace AVS.Trading.Engine.Emulator
{
    public class TradingAlgorithmException:Exception
    {
        public TradingAlgorithmException()
        {
        }

        public TradingAlgorithmException(string message) : base(message)
        {
        }

        public TradingAlgorithmException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
    
    public class AlgorithmContextException : TradingAlgorithmException
    {
        public AlgorithmContextException()
        {
        }

        public AlgorithmContextException(string message) : base(message)
        {
        }

        public AlgorithmContextException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}