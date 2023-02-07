using System;
using System.Globalization;

namespace AVS.Trading.Core.Extensions
{
    public static class MathExtensions
    {
        public static double Lowest(this double v1, double v2)
        {
            return v1 <= v2 ? v1 : v2;
        }
        public static double Highest(this double v1, double v2)
        {
            return v1 >= v2 ? v1 : v2;
        }
        
        public static bool WithinRange(this int value, int from, int to)
        {
            return value >= from && value <= to;
        }

        public static bool WithinRange(this double value, double from, double to)
        {
            return value >= from && value <= to;
        }

        public static bool Eq(this double value, double valueToCompare, double tolerance = 0.00000001)
        {
            return Math.Abs(value - valueToCompare) < tolerance;
        }

        /// <summary>
        /// calculates distance as greater number / smaller number  - 1  
        /// </summary> 
        /// <remarks>
        /// formula: Vg*(1+x)=Vs  => x = (Vg-Vs)/Vs => x = Vg/Vs-1
        /// e.g. Distance(0.5,0.6) = 0.6/0.5-1 = 0.1 => 10%
        /// </remarks>
        public static double Distance(this double value1, double value2)
        {
            if (value1 > value2)
                return (value1) / value2 - 1;
            return (value2) / value1 - 1;
        }

        public static bool WithinDistance(this double value1, double value2, double dist)
        {
            return value1.Distance(value2) < dist;
        }

    }
}