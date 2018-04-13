using System;
using System.Collections.Generic;
using System.Text;

namespace S2p.RestClient.Sdk.Entities
{
    public class CardRefundStatusResponse
    {
        public long? ID { get; set; }
        public long? InitialPaymentID { get; set; }
        public string MerchantTransactionID { get; set; }
        public CardStateDetails StateDetails { get; set; }
    }
}
