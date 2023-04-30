namespace Shearlegs.Web.APIClient.Models.NodeVariables.Requests
{
    public class UpdateNodeVariableRequest
    {
        public string Name { get; set; }
        public string Value { get; set; }
        public string Description { get; set; }
        public bool IsSensitive { get; set; }
    }
}
