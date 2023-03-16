using Shearlegs.Web.API.Models.Results;
using System.Text;

namespace Shearlegs.Web.API.Services.Processings.Results
{
    public partial class ResultProcessingService
    {
        private ResultParameters MapResultParametersDataToResultParameters(ResultParametersData resultParametersData)
        {
            return new ResultParameters()
            {
                ParametersJson = Encoding.UTF8.GetString(resultParametersData.ParametersData)
            };
        }

        private ResultContent MapResultContentDataToResultContent(ResultContentData resultContentData) 
        {
            return new ResultContent()
            {
                ContentJson = Encoding.UTF8.GetString(resultContentData.ResultData)
            };
        }
    }
}
