using Shearlegs.Web.API.Brokers.Storages;
using Shearlegs.Web.API.Models.Results;
using Shearlegs.Web.API.Models.Results.Exceptions;
using Shearlegs.Web.API.Models.Results.Params;
using Shearlegs.Web.API.Models.Results.Results;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.Results
{
    public class ResultService : IResultService
    {
        private readonly IStorageBroker storageBroker;

        public ResultService(IStorageBroker storageBroker)
        {
            this.storageBroker = storageBroker;
        }

        public async ValueTask<Result> RetrieveResultByIdAsync(int resultId)
        {
            GetResultsParams @params = new()
            {
                ResultId = resultId
            };

            Result result = await storageBroker.GetResultAsync(@params);

            if (result == null)
            {
                throw new NotFoundResultException();
            }

            return result;
        }

        public async ValueTask<IEnumerable<Result>> RetrieveAllResultsAsync()
        {
            GetResultsParams @params = new();

            return await storageBroker.GetResultsAsync(@params);
        }

        public async ValueTask<Result> AddResultAsync(AddResultParams @params)
        {
            AddResultResult result = await storageBroker.AddResultAsync(@params);

            return await RetrieveResultByIdAsync(result.ResultId.Value);
        }

        public async ValueTask<Result> UpdateResultAsync(UpdateResultParams @params)
        {
            StoredProcedureResult result = await storageBroker.UpdateResultAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new NotFoundResultException();
            }

            return await RetrieveResultByIdAsync(@params.ResultId);
        }

        public async ValueTask<Result> UpdateResultStatusAsync(UpdateResultStatusParams @params)
        {
            StoredProcedureResult result = await storageBroker.UpdateResultStatusAsync(@params);

            if (result.ReturnValue == 1)
            {
                throw new NotFoundResultException();
            }

            return await RetrieveResultByIdAsync(@params.ResultId);
        }
    }
}
