using System;
using System.Collections.Generic;
using AVS.CoreLib._System.Net;
using AVS.CoreLib.ClientApi;
using AVS.CoreLib.ClientApi.WebClients;
using AVS.CoreLib.Utils;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.Interfaces.LendingTools;
using AVS.Trading.Core.ResponseModels;
using AVS.Trading.Core.ResponseModels.TradingTools;
using AVS.PoloniexApi.LendingTools.Models;
using AVS.PoloniexApi.PoloniexCommands;
using AVS.PoloniexApi.WalletTools.Models;

namespace AVS.PoloniexApi.LendingTools
{
    public class LendingApi : ApiToolsBase, ILendingApi
    {
        protected PublicApiWebClient PublicApiWebClient { get; set; }
        public LendingApi(PublicApiWebClient publicApiWebClient, PrivateApiWebClient privateApiWebClient) : base(privateApiWebClient)
        {
            PublicApiWebClient = publicApiWebClient;
        }

        public Response<ILoanOrders> GetMarketLoanOrders(string currency)
        {
            var jsonResult = Execute(LendingToolsCommands.ReturnLoanOrders, RequestData.Create("currency=" + currency));

            var projection = jsonResult.AsObject<ILoanOrders>();
            Response<ILoanOrders> result = projection.Map<LoanOrders>(data=> data.Currency = currency);
            return result;
        }

        public Response<IDictionary<string, IList<IOpenLoanOffer>>> GetOpenLoanOffers()
        {
            var jsonResult = Execute(LendingToolsCommands.ReturnOpenLoanOffers, RequestData.Empty);

            var projection = jsonResult.AsDictionary<string, IList<IOpenLoanOffer>>();
            var res = projection.Map<List<OpenLoanOffer>>();
            return res;
        }
        
        public SimpleResponse ToogleAutoRenew(long activeLoanId = 329642757)
        {
            var jsonResult = Execute(LendingToolsCommands.ToogleAutoRenew, RequestData.Create("orderNumber=" + activeLoanId));
            var response = jsonResult.Deserialize<SimpleResponse>();
            return response;
        }
        
        public SimpleResponse CancelLoanOffer(long loanId)
        {
            var jsonResult = Execute(LendingToolsCommands.CancelLoanOffer, RequestData.Create("orderNumber=" + loanId));
            var response = jsonResult.Deserialize<SimpleResponse>();
            return response;
        }

        public CreateLoanOfferResponse CreateLoanOffer(string currency, double amount, double rate, int duration, bool autorenew)
        {
            var postData = new Dictionary<string, object>
            {
                {"currency", currency},
                {"amount", amount},
                {"duration", duration},
                {"autoRenew", autorenew ? "1":"0"},
                {"lendingRate", rate}
            };

            var jsonResult = Execute(LendingToolsCommands.CreateLoanOffer, postData);
            var poloniexResponse = jsonResult.Deserialize<CreateLoanOfferPoloniexResponse>();

            CreateLoanOfferResponse response = new CreateLoanOfferResponse
            {
                Error = poloniexResponse.Error
            };

            if (poloniexResponse.Success)
            {
                response.AutoRenew = autorenew;
                response.Amount = amount;
                response.Currency = currency;
                response.DateUtc = DateTime.Now;
                response.Duration = duration;
                response.Id = poloniexResponse.OfferNumber;
                response.Rate = rate;
            }

            return response;
        }

        public Response<IDictionary<string, IList<IActiveLoan>>> GetActiveLoans()
        {
            var jsonResult = Execute(LendingToolsCommands.ReturnActiveLoans, RequestData.Empty);
            var projection = jsonResult.AsDictionary<string, IList<IActiveLoan>>();

            var res = projection.Map<ActiveLoan>();
            return res;

            //var response = cmd.Execute<IDictionary<string, IList<ActiveLoan>>>(RequestData.Empty);
            //var res = response.Cast<ActiveLoan, IActiveLoan>();
            //return res;
        }

        public double GetMinLendingAmount(string currency)
        {
            switch (currency)
            {
                case "BTC":
                     return 0.01;
                default:
                    return 1.0;
            }
        }
    }

    
}
