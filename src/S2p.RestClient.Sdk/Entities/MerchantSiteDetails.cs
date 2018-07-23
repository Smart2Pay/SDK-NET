using System.Collections.Generic;

namespace S2p.RestClient.Sdk.Entities
{
    public class MerchantSiteDetails
    {
        public int ID { get; set; }
        public bool ShouldSerializeID()
        {
            return ID > 0;
        }
        public string Info { get; set; }
        public bool ShouldSerializeInfo()
        {
            return !string.IsNullOrEmpty(Info);
        }
        public List<MerchantSiteReason> Reasons { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string BankName { get; set; }
        public string AccountIBAN { get; set; }
        public string AccountSWIFT { get; set; }
        public string BankSWIFTID { get; set; }
        public string BankCode { get; set; }
        public string VATNumber { get; set; }
        public string RegistrationNumber { get; set; }
        public string MCC { get; set; }
        public string MainBusiness { get; set; }
    }
}
