using System;

namespace Shearlegs.Web.API.Models.NodeDaemons.Exceptions
{
    public class NodeDaemonCommunicationException : Exception
    {
        public NodeDaemonCommunicationException(Exception innerException)
            : base("Node daemon communication error occurred.", innerException)
        {
        }
    }
}
