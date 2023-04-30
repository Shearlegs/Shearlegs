using Shearlegs.Web.API.Models.NodeVariables.Params;

namespace Shearlegs.Web.API.Models.NodeVariableUserAuthentications.Params
{
    public class UpdateUserNodeVariableParams
    {
        public int NodeVariableId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool IsSensitive { get; set; }
    }
}
