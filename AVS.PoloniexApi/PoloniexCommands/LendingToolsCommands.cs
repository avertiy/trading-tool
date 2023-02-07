namespace AVS.PoloniexApi.PoloniexCommands
{
    public static class LendingToolsCommands
    {
        /// <summary>
        /// это мои оферы кому надо взаймы
        /// </summary>
        public const string ReturnOpenLoanOffers = "returnOpenLoanOffers";

        /// <summary>
        /// это лоаны которые я взял взаймы для маржинальной торговли
        /// </summary>
        public const string ReturnActiveLoans = "returnActiveLoans";

        public const string ReturnLendingHistory = "returnLendingHistory";

        /// <summary>
        /// Toggles the autoRenew setting on an active loan, specified by the "orderNumber" POST parameter. 
        /// If successful, "message" will indicate the new autoRenew setting. 
        /// JSON response: {"success": 1, "message": 0}
        /// </summary>
        public const string ToogleAutoRenew = "toggleAutoRenew";

        /// <summary>
        /// Returns the list of loan offers and demands for a given currency, specified by the "currency" GET parameter.
        /// </summary>
        public const string ReturnLoanOrders = "returnLoanOrders";
        /// <summary>
        /// Cancels a loan offer specified by the "orderNumber" POST parameter.
        /// </summary>
        public const string CancelLoanOffer = "cancelLoanOffer";
        /// <summary>
        /// Creates a loan offer for a given currency. 
        /// Required POST parameters are "currency", "amount", "duration", "autoRenew" (0 or 1), and "lendingRate".
        /// </summary>
        public const string CreateLoanOffer = "createLoanOffer";
    }
}