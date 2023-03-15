using Shearlegs.Web.API.Models.Results;
using Shearlegs.Web.API.Models.Results.Params;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.Results
{
    public interface IResultService
    {
        ValueTask<Result> AddResultAsync(AddResultParams @params);
        ValueTask<IEnumerable<Result>> RetrieveAllResultsAsync();
        ValueTask<Result> RetrieveResultByIdAsync(int resultId);
        ValueTask<ResultContentData> RetrieveResultContentDataByIdAsync(int resultId);
        ValueTask<ResultParametersData> RetrieveResultParametersDataByIdAsync(int resultId);
        ValueTask<Result> UpdateResultAsync(UpdateResultParams @params);
        ValueTask<Result> UpdateResultStatusAsync(UpdateResultStatusParams @params);
    }
}