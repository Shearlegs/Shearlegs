namespace Shearlegs.Web.API.Models.NodeVariables.Params
{
    public class UpdateNodeVariableParams
    {
        public int NodeVariableId { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool IsSensitive { get; set; }
        public int UpdateUserId { get; set; }
    }
}
