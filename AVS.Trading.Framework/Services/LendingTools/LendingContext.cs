using System.Collections.Generic;
using AVS.Trading.Core.Interfaces.LendingTools;
using AVS.Trading.Core.ResponseModels.TradingTools;

namespace AVS.Trading.Framework.Services.LendingTools
{
    public class LendingContext
    {
        public const double LendingLimitKoef = 0.95;
        public IDictionary<string, IList<IOpenLoanOffer>> AllOffers { get; set; }
        public IList<IOpenLoanOffer> OpenLoanOffers => AllOffers[TargetCurrency];

        public IList<IActiveLoan> ProvidedLoans { get; set; }
        
        public IAvailableAccountBalances Balances { get; set; }
        public IList<LoanOrderItem> MarketLoanOffers { get; set; }
        public IList<LoanOrderItem> MarketLoanDemands { get; set; }
        
        public double MinLendingAmount { get; set; }
        public double MinLendingRate { get; set; }

        public double TotalAvailableAmount
        {
            get
            {
                var total = Balances.Exchange[TargetCurrency] +
                            Balances.Margin[TargetCurrency] +
                            Balances.Lending[TargetCurrency];
                return total * LendingLimitKoef;
            }
        }

        public int LendingCapacity => (int)(TotalAvailableAmount / MinLendingAmount);
        
        public string TargetCurrency { get; set; }
        public double TargetRate { get; set; }
        public int Duration { get; set; }
    }
}