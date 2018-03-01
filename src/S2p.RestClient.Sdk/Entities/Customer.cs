namespace S2p.RestClient.Sdk.Entities
{
    public class Customer
    {
        public int ID { get; set; }
        public bool ShouldSerializeID()
        {
            return ID > 0;
        }
        public string MerchantCustomerID { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string SocialSecurityNumber { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }
        public string DateOfBirth { get; set; }
    }
}
