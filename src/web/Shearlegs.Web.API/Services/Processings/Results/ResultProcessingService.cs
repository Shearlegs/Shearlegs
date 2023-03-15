using Shearlegs.Web.API.Models.Results;
using Shearlegs.Web.API.Services.Foundations.Results;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Processings.Results
{
    public partial class ResultProcessingService : IResultProcessingService
    {
        private readonly IResultService resultService;

        public ResultProcessingService(IResultService resultService)
        {
            this.resultService = resultService;
        }

        public async ValueTask<ResultParameters> RetrieveResultParametersByIdAsync(int resultId)
        {
            ResultParametersData resultParametersData = await resultService.RetrieveResultParametersDataByIdAsync(resultId);
            ResultParameters resultParameters = MapResultParametersDataToResultParameters(resultParametersData);

            return resultParameters;
        }
    }
}
