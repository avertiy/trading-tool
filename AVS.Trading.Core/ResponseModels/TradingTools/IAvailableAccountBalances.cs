using System.Collections.Generic;

namespace AVS.Trading.Core.ResponseModels.TradingTools
{
    public interface IAvailableAccountBalances
    {
        IDictionary<string, double> Exchange { get;  }
        IDictionary<string, double> Margin { get;  }
        IDictionary<string, double> Lending { get;  }
    }
}