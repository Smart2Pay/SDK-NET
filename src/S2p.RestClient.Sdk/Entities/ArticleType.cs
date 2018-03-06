namespace S2p.RestClient.Sdk.Entities
{
    // 1 - 4 are used for Klarna Invoice
    // 4 - 11 are used for Klarna Payments
    // see Type field in Articles node in sample at: https://docs.smart2pay.com/specific-capture-status-codes-for-klarna-payments/
    public static class ArticleType
    {
        public const int Product = 1;
        public const int Shipping = 2;
        public const int Handling = 3;
        public const int Discount = 4;
        public const int Physical = 5;
        public const int ShippingFree = 6;
        public const int SalesTax = 7;
        public const int Digital = 8;
        public const int GiftCard = 9;
        public const int StoreCredit = 10;
        public const int Surcharge = 11;
    }
}
