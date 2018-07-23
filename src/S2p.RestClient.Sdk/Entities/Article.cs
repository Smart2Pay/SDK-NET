using Newtonsoft.Json;

namespace S2p.RestClient.Sdk.Entities
{
    public class Article
    {
        public string MerchantArticleID { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        [JsonConverter(typeof(DecimalFormatConverter))]
        public decimal Price { get; set; }
        [JsonConverter(typeof(DecimalFormatConverter))]
        public decimal VAT { get; set; }
        [JsonConverter(typeof(DecimalFormatConverter))]
        public decimal Discount { get; set; }
        public int Type { get; set; }
        [JsonConverter(typeof(DecimalFormatConverter))]
        public decimal DiscountValue { get; set; }
        public int TaxType { get; set; }
    }
}
