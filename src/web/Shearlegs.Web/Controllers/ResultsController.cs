using Microsoft.AspNetCore.Mvc;
using Shearlegs.Core.Plugins.Result;
using Shearlegs.Web.Constants;
using Shearlegs.Web.Database.Repositories;
using Shearlegs.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ResultsController : ControllerBase
    {
        private readonly ResultsRepository resultsRepository;
        
        public ResultsController(ResultsRepository resultsRepository)
        {
            this.resultsRepository = resultsRepository;
        }

        [HttpGet("{resultId}/file")]
        public async Task<IActionResult> GetFileAsync(int resultId)
        {
            MResult result = await resultsRepository.GetResultAsync(resultId);
            if (result.ResultType != ResultConstants.FileResult)
            {
                return BadRequest();
            }

            PluginFileResult fileResult = result.Deserialize<PluginFileResult>();
            return File(fileResult.Content, fileResult.MimeType, fileResult.Name);
        }
    }
}
