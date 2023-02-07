using System;
using System.Linq;
using AVS.Trading.Core.Extensions;
using AVS.Trading.Core.ResponseModels.TradingTools;

namespace AVS.Trading.Framework.Services.LendingTools
{
    public interface ILendingContextAnalizer
    {
        bool ShouldPlaceLoanOffer(LendingContext ctx);
        double MinLendingRate { get; }
    }

    public class DefaultLendingContextAnalizer : ILendingContextAnalizer
    {
                                                    //rank: 1      2      3     4     5     6     7      8    9     10
        private double[] _loanRatesScale = new double[] { 0.014, 0.025, 0.05, 0.075, 0.1, 0.14, 0.175, 0.2, 0.25, 0.3 };

        public double MinLendingRate => _loanRatesScale.First();
        private int MaxRank => _loanRatesScale.Length;
        
        public virtual bool ShouldPlaceLoanOffer(LendingContext ctx)
        {
            var offers = ctx.MarketLoanOffers.SkipWhile(o => o.Rate < MinLendingRate).ToArray();
            var rate = offers.First(o => o.Rate >= MinLendingRate).Rate;
            var rank = GetRank(rate);

            if (!ShouldProvideOffer(ctx, rank))
                return false;

            ctx.TargetRate = GetBestRate(offers, rank);
            ctx.Duration = GetDuration(ctx, rank);
            return true;
        }

        protected virtual bool ShouldProvideOffer(LendingContext ctx, int rank)
        {
            if (rank == 0)
                return false;

            //var rate1 = _loanRatesScale[rank];
            var rate2 = _loanRatesScale[rank + 1];

            //if there is any recent provided loan with a rate by the next rank, i.e. by a better rate than current 
            //it's better wait because it could be that better rate conditions might come soon
            if (ctx.ProvidedLoans.Any(loan =>
                    loan.Rate >= rate2 && loan.DateUtc.AddDays(1) > DateTime.Now) && ctx.LendingCapacity < 4)
            {
                return false;
            }

            var loanOffersSameRankCount = ctx.OpenLoanOffers.Count(o => GetRank(o.Rate) == rank);
            var providedLoansSameRankCount = ctx.ProvidedLoans.Count(o => GetRank(o.Rate) == rank);

            if (loanOffersSameRankCount == 0)
            {
                if (rank == MaxRank && ctx.LendingCapacity > 0)
                    return true;

                if (providedLoansSameRankCount == 0)
                    return true;

                if (providedLoansSameRankCount < 3)
                {
                    if (rank > 5 && ctx.LendingCapacity > 2)
                        return true;

                    if (rank > 4 && ctx.LendingCapacity > 3)
                        return true;

                    if (rank > 3 && ctx.LendingCapacity > 4)
                        return true;
                }

                return false;
            }

            if (providedLoansSameRankCount == 0)
            {
                if (rank > 5 && loanOffersSameRankCount < 2)
                    return true;
            }

            //identify other cases when possible to open load offer 
            return false;
        }

        protected virtual int GetDuration(LendingContext ctx, int rank)
        {
            if (rank >= 5)
                return 60;
            if (rank >= 3)
                return 30;
            if (ctx.LendingCapacity > 4)
                return 30;
            return 5;
        }
        
        protected double GetBestRate(LoanOrderItem[] offers, int rank)
        {
            //select offers by rank
            var offersByRank = offers.Where(o => o.Rate <= _loanRatesScale[rank]).ToList();
            var index = offersByRank.GetIndexOfBestValue(o => o.Rate, bestFromRank: 5);
            var targetRate = offersByRank[index].Rate;
            return targetRate;
        }

        protected int GetRank(double rate)
        {
            return rate.GetRankByScale(_loanRatesScale);
        }

        public void SetLoanRatesScale(double[] scale)
        {
            _loanRatesScale = scale;
        }

    }
}