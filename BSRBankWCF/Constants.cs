namespace BSRBankWCF
{
    /// <summary>
    /// Set of constants used within the service
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Database  connection string
        /// </summary>
        public static readonly string DatabaseUri = "mongodb://localhost:27017";

        /// <summary>
        /// Database name
        /// </summary>
        public static readonly string DatabaseName = "BSR";
        
        /// <summary>
        /// Database user collection name
        /// </summary>
        public static readonly string UserCollection = "user_collection";

        /// <summary>
        /// Database history collection name
        /// </summary>
        public static readonly string HistoryCollection = "history_collection";
        
        /// <summary>
        /// Service bank id
        /// </summary>
        public static readonly string BankId = "00109562";

        /// <summary>
        /// External transfer operation type string
        /// </summary>
        public static readonly string ExternalTransferType = "Przelew zewnętrzny";
        /// <summary>
        /// Internal transfer operation type string
        /// </summary>
        public static readonly string InternalTransferType = "Przelew wewnętrzny";
        /// <summary>
        /// Withdraw or deposit operation type string
        /// </summary>
        public static readonly string WithdrawDepositType = "wypłata/wpłata";
    }
}