using Shearlegs.Web.API.Models.Results;
using Shearlegs.Web.API.Models.Results.Params;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Processings.Results
{
    public interface IResultProcessingService
    {
        ValueTask<Result> AddResultAsync(AddResultParams @params);
        ValueTask<Result> RetrieveResultByIdAsync(int resultId);
        ValueTask<ResultContent> RetrieveResultContentByIdAsync(int resultId);
        ValueTask<ResultParameters> RetrieveResultParametersByIdAsync(int resultId);
        ValueTask<Result> UpdateResultAsync(UpdateResultParams @params);
        ValueTask<Result> UpdateResultStatusAsync(UpdateResultStatusParams @params);
    }
}