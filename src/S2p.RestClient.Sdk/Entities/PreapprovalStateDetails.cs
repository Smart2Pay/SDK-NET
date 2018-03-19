using System.Collections.Generic;

namespace S2p.RestClient.Sdk.Entities
{
    public class PreapprovalStateDetails
    {
        public byte ID { get; set; }
        public string Info { get; set; }
        public List<PreapprovalStateReason> Reasons { get; set; }
    }
}
