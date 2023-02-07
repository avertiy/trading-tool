using System.Collections.Generic;
using AVS.CoreLib._System.Net;
using AVS.Trading.Core.ResponseModels;
using AVS.Trading.Core.ResponseModels.TradingTools;

namespace AVS.Trading.Core.Interfaces.LendingTools
{
    public interface ILendingApi
    {
        Response<ILoanOrders> GetMarketLoanOrders(string currency);
        /// <summary>
        /// Returns my Open Loan Offers
        /// </summary>
        Response<IDictionary<string, IList<IOpenLoanOffer>>> GetOpenLoanOffers();
        SimpleResponse CancelLoanOffer(long loanId);
        CreateLoanOfferResponse CreateLoanOffer(string currency, double amount, double rate, int duration, bool autorenew);
        SimpleResponse ToogleAutoRenew(long orderNumber);

        Response<IDictionary<string, IList<IActiveLoan>>> GetActiveLoans();
        double GetMinLendingAmount(string currency);
    }
}