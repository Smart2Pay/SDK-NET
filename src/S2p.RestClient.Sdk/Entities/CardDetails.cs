namespace S2p.RestClient.Sdk.Entities
{
    public class CardDetails
    {   
        public int ID { get; set; }
        public bool ShouldSerializeID()
        {
            return ID > 0;
        }
        public string HolderName { get; set; }
        public string Number { get; set; }
        public string Type { get; set; }
        public string ExpirationMonth { get; set; }
        public string ExpirationYear { get; set; }
        public string SecurityCode { get; set; }
        public string Installments { get; set; }
    }
}
