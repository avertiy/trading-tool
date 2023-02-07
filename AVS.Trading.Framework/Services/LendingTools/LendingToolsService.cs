using System.Collections.Generic;
using System.Linq;
using AVS.CoreLib._System.Net;
using AVS.Trading.Framework.Infrastructure;
using AVS.Trading.Core.Interfaces.LendingTools;
using AVS.Trading.Core.ResponseModels;
using AVS.Trading.Core.ResponseModels.TradingTools;

namespace AVS.Trading.Framework.Services.LendingTools
{
    public interface ILendingToolsService
    {
        Response<IDictionary<string, IList<IActiveLoan>>> GetActiveLoans();
        IList<IActiveLoan> GetUsedActiveLoans(string currency);
        IList<IActiveLoan> GetProvidedActiveLoans(string currency);
        bool ToogleActiveLoanAutoRenew(long orderNumber);

        /// <summary>
        /// All my open loan offers
        /// </summary>
        Response<IDictionary<string, IList<IOpenLoanOffer>>> GetOpenLoanOffers();
        /// <summary>
        /// my open loan offers
        /// </summary>
        Response<IList<IOpenLoanOffer>> GetOpenLoanOffers(string currency);

        SimpleResponse CancelLoanOffer(long offerNumber);
        CreateLoanOfferResponse CreateLoanOffer(string currency, double amount, double rate, int duration, bool autorenew);

        Response<ILoanOrders> GetMarketLoanOrders(string currency);
        double GetMinLendingAmount(string currency);
    }

    public class LendingToolsService : ExchangeServiceBase, ILendingToolsService
    {
        protected ILendingApi LendingApi => Client.LendingTools;

        public LendingToolsService(IWorkContext workContext) : base(workContext)
        {
        }
        
        
        public Response<IDictionary<string, IList<IActiveLoan>>> GetActiveLoans()
        {
            return LendingApi.GetActiveLoans();
        }
        
        public IList<IActiveLoan> GetUsedActiveLoans(string currency)
        {
            var response = LendingApi.GetActiveLoans();
            var offers = response.Success ? response.Data["used"] : new List<IActiveLoan>();
            return string.IsNullOrEmpty(currency) ? offers : offers.Where(o=>o.Currency == currency).ToList();
        }

        public IList<IActiveLoan> GetProvidedActiveLoans(string currency)
        {
            var response = LendingApi.GetActiveLoans();
            var offers = response.Success ? response.Data["provided"] : new List<IActiveLoan>();
            return string.IsNullOrEmpty(currency) ? offers : offers.Where(o => o.Currency == currency).ToList();
        }
  
        public bool ToogleActiveLoanAutoRenew(long orderNumber)
        {
            SimpleResponse response = LendingApi.ToogleAutoRenew(orderNumber);
            return response.Success;
        }

        public Response<IDictionary<string, IList<IOpenLoanOffer>>> GetOpenLoanOffers()
        {
            return LendingApi.GetOpenLoanOffers();
        }

        public Response<IList<IOpenLoanOffer>> GetOpenLoanOffers(string currency)
        {
            Response<IDictionary<string, IList<IOpenLoanOffer>>> response = GetOpenLoanOffers();
            var res = response.To<Response<IList<IOpenLoanOffer>>>( (data, result) => result.Data = data[currency]);
            return res;
        }

        public SimpleResponse CancelLoanOffer(long offerNumber)
        {
            return LendingApi.CancelLoanOffer(offerNumber);
        }

        public CreateLoanOfferResponse CreateLoanOffer(string currency, double amount, double rate, int duration, bool autorenew)
        {
            return LendingApi.CreateLoanOffer(currency, amount, rate, duration, autorenew);
        }

        public Response<ILoanOrders> GetMarketLoanOrders(string currency)
        {
            return LendingApi.GetMarketLoanOrders(currency);
        }

        public double GetMinLendingAmount(string currency)
        {
            return LendingApi.GetMinLendingAmount(currency);
        }
    }
}

