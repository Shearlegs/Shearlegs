namespace Shearlegs.Web.API.Models.NodeVariableUserAuthentications.Params
{
    public class AddUserNodeVariableParams
    {
        public int NodeId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool IsSensitive { get; set; }
    }
}
