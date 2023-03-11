namespace Shearlegs.Web.API.Services.Foundations.Results
{
    public class ResultService : IResultService
    {
        private readonly IResultService resultService;

        public ResultService(IResultService resultService)
        {
            this.resultService = resultService;
        }
    }
}
