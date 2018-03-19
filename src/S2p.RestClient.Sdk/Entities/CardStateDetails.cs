using System.Collections.Generic;

namespace S2p.RestClient.Sdk.Entities
{
    public class CardStateDetails
    {
        public byte? ID { get; set; }
        public string Info { get; set; }
        public List<CardStateReason> Reasons { get; set; }
    }
}
