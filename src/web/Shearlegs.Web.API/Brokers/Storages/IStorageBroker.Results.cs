using Shearlegs.Web.API.Models.Results;
using Shearlegs.Web.API.Models.Results.Params;
using Shearlegs.Web.API.Models.Results.Results;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<AddResultResult> AddResultAsync(AddResultParams @params);
        ValueTask<StoredProcedureResult> UpdateResultAsync(UpdateResultParams @params);
        ValueTask<StoredProcedureResult> UpdateResultStatusAsync(UpdateResultStatusParams @params);
        ValueTask<IEnumerable<Result>> GetResultsAsync(GetResultsParams @params);
        ValueTask<Result> GetResultAsync(GetResultsParams @params);
        ValueTask<ResultContentData> SelectResultContentDataByIdAsync(int resultId);
        ValueTask<ResultParametersData> SelectResultParametersDataByIdAsync(int resultId);
    }
}
