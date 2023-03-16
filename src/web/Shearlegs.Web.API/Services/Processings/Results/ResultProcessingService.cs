using Shearlegs.Web.API.Models.Results;
using Shearlegs.Web.API.Models.Results.Params;
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

        public async ValueTask<Result> AddResultAsync(AddResultParams @params)
        {
            return await resultService.AddResultAsync(@params);
        }

        public async ValueTask<Result> RetrieveResultByIdAsync(int resultId)
        {
            return await resultService.RetrieveResultByIdAsync(resultId);
        }

        public async ValueTask<Result> UpdateResultStatusAsync(UpdateResultStatusParams @params)
        {
            return await resultService.UpdateResultStatusAsync(@params);
        }

        public async ValueTask<Result> UpdateResultAsync(UpdateResultParams @params)
        {
            return await resultService.UpdateResultAsync(@params);
        }

        public async ValueTask<ResultParameters> RetrieveResultParametersByIdAsync(int resultId)
        {
            ResultParametersData resultParametersData = await resultService.RetrieveResultParametersDataByIdAsync(resultId);
            ResultParameters resultParameters = MapResultParametersDataToResultParameters(resultParametersData);

            return resultParameters;
        }

        public async ValueTask<ResultContent> RetrieveResultContentByIdAsync(int resultId)
        {
            ResultContentData resultContentData = await resultService.RetrieveResultContentDataByIdAsync(resultId);
            ResultContent resultContent = MapResultContentDataToResultContent(resultContentData);

            return resultContent;
        }
    }
}
