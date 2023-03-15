using Shearlegs.Web.API.Models.Results;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Processings.Results
{
    public interface IResultProcessingService
    {
        ValueTask<ResultParameters> RetrieveResultParametersByIdAsync(int resultId);
    }
}