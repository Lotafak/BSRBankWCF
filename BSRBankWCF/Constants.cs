namespace BSRBankWCF
{
    public static class Constants
    {
        public static readonly string DatabaseUri = "mongodb://localhost:27017";
        public static readonly string DatabaseName = "BSR";

        public static readonly string AccountNumber = "91001095625642845915422653";
        public static readonly string UserCollection = "user_collection";
        public static readonly string HistoryCollection = "history_collection";

        public static readonly string CountryCode = "PL";
        public static readonly string BankId = "00109562";

        public static readonly string ExternalTransferType = "Przelew zewnętrzny";
        public static readonly string InternalTransferType = "Przelew wewnętrzny";
        public static readonly string WithdrawDepositType = "wypłata/wpłata";

    }
}