namespace Shearlegs.Web.API.Models.NodeVariables.Params
{
    public class AddNodeVariableParams
    {
        public int NodeId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool IsSensitive { get; set; }
        public int CreateUserId { get; set; }
    }
}
