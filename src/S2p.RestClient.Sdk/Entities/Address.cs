namespace S2p.RestClient.Sdk.Entities
{
    public class Address
    {
        public int ID { get; set; }
        public bool ShouldSerializeID()
        {
            return ID > 0;
        }
        public string City { get; set; }
        public string ZipCode { get; set; }
        public string State { get; set; }
        public string Street { get; set; }
        public string StreetNumber { get; set; }
        public string HouseNumber { get; set; }
        public string HouseExtension { get; set; }
        public string Country { get; set; }
    }
}
