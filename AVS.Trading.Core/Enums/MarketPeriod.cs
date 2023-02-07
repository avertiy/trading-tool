namespace AVS.Trading.Core.Enums
{
    /// <summary>Represents a time frame of a market.</summary>
    public enum MarketPeriod
    {
        /// <summary>A time interval of 5 minutes.</summary>
        M5 = 300,

        /// <summary>A time interval of 15 minutes.</summary>
        M15 = 900,

        /// <summary>A time interval of 30 minutes.</summary>
        M30 = 1800,

        H = 3600,
        /// <summary>A time interval of 2 hours.</summary>
        H2 = 7200,

        /// <summary>A time interval of 4 hours.</summary>
        H4 = 14400,

        /// <summary>A time interval of a day.</summary>
        D = 86400
    }
}
