namespace S2p.RestClient.Sdk.Entities
{
    public class DisputeResponse
    {           
        public int? ID { get; set; }
        public int? SiteID { get; set; }
        public string Created { get; set; }
        public int PaymentID { get; set; }
        public short? MethodID { get; set; }
        public long? Amount { get; set; }
        public string Currency { get; set; }
        public PaymentStateDetails Status { get; set; }
    }
}
