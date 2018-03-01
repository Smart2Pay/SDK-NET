using System.Collections.Generic;

namespace S2p.RestClient.Sdk.Entities
{
    public class PaymentStateDetails
    {
        public byte? ID { get; set; }

        public string Info { get; set; }

        public List<PaymentStateReason> Reasons { get; set; }
    }
}
