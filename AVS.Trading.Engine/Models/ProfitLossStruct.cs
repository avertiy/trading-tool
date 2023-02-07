using System.Text;
using AVS.Trading.Core.Extensions;

namespace AVS.Trading.Engine.Models
{
    
    public struct ProfitLossStruct
    {
        public double Amount;
        public double ProfitLoss;
        /// <summary>
        /// rest amount if positive to be sold otherwise to be bought
        /// </summary>
        public double Rest;
        /// <summary>
        /// the price the rest amount 
        /// </summary>
        public double RestPrice;
        public double RestTotal => Rest * RestPrice;

        public override string ToString()
        {
            return $"P&L: {Amount.FormatAsQuantity()} - {ProfitLoss.FormatAsPrice()}";
        }

        public string ToDetailsString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"P&L: {Amount.FormatAsQuantity()} - {ProfitLoss.FormatAsPrice()}");

            if(Rest > 0)
                sb.AppendLine($"Rest: {Rest.FormatAsQuantity()} to be sold from {RestPrice.FormatAsPrice()}");
            else
                sb.AppendLine($"Rest: {Rest.FormatAsQuantity()} to be bought from {RestPrice.FormatAsPrice()}");
            return sb.ToString();
        }
    }
}