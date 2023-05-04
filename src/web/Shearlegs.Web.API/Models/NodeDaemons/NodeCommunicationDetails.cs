namespace Shearlegs.Web.API.Models.NodeDaemons
{
    public class NodeCommunicationDetails
    {
        public string FQDN { get; set; }
        public string Scheme { get; set; }
        public int HttpPort { get; set; }
        public int HttpsPort { get; set; }
        public bool IsBehindProxy { get; set; }

        public string GetBaseAddress()
        {
            if (IsBehindProxy)
            {
                return $"{Scheme}://{FQDN}";
            }
            else
            {
                if (Scheme == "https")
                {
                    return $"{Scheme}://{FQDN}:{HttpsPort}";
                } else
                {
                    return $"{Scheme}://{FQDN}:{HttpPort}";
                }                
            }
        }
    }
}
