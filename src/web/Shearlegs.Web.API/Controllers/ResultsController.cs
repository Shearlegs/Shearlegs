using Microsoft.AspNetCore.Mvc;
using RESTFulSense.Controllers;
using Shearlegs.Web.API.Models.Results;
using Shearlegs.Web.API.Models.Results.Exceptions;
using Shearlegs.Web.API.Services.Foundations.Results;
using Shearlegs.Web.API.Services.Processings.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Controllers
{
    [ApiController]
    [Route("results")]
    public class ResultsController : RESTFulController
    {
        private readonly IResultService resultService;
        private readonly IResultProcessingService resultProcessingService;

        public ResultsController(IResultService resultService, IResultProcessingService resultProcessingService)
        {
            this.resultService = resultService;
            this.resultProcessingService = resultProcessingService;
        }

        [HttpGet]
        public async ValueTask<IActionResult> GetResults()
        {
            IEnumerable<Result> results = await resultService.RetrieveAllResultsAsync();

            return Ok(results);
        }

        [HttpGet("{resultId}")]
        public async ValueTask<IActionResult> GetResultById(int resultId)
        {
            try
            {
                Result result = await resultService.RetrieveResultByIdAsync(resultId);

                return Ok(result);
            } catch (NotFoundResultException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpGet("{resultId}/parameters")]
        public async ValueTask<IActionResult> GetResultParametersById(int resultId)
        {
            try
            {
                ResultParameters resultParameters = await resultProcessingService.RetrieveResultParametersByIdAsync(resultId);

                return Content(resultParameters.ParametersJson, "application/json");
            } catch (NotFoundResultException exception)
            {
                return NotFound(exception);
            }
        }

        [HttpGet("{resultId}/content")]
        public async ValueTask<IActionResult> GetResultContentById(int resultId)
        {
            try
            {
                ResultContent resultContent = await resultProcessingService.RetrieveResultContentByIdAsync(resultId);

                return Content(resultContent.ContentJson, "application/json");
            }
            catch (NotFoundResultException exception)
            {
                return NotFound(exception);
            }
        }
    }
}
