
using AVS.CoreLib._System.Net;

namespace AVS.Trading.Core.ResponseModels
{
    public interface IExchangeResponse : IResponse
    {
    }

    public class ExchangeResponse: Response, IExchangeResponse
    {
    }
}