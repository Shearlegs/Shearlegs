using Shearlegs.Web.API.Services.Foundations.Schedulings;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.Schedulings
{
    public class SchedulingOrchestrationService : ISchedulingOrchestrationService
    {
        private readonly ISchedulingService schedulingService;

        public SchedulingOrchestrationService(ISchedulingService schedulingService)
        {
            this.schedulingService = schedulingService;
        }

        public async ValueTask<string> EnqueueAsync(Expression<Func<Task>> methodCall)
        {
            string jobId = await schedulingService.EnqueueAsync(methodCall);

            return jobId;
        }
    }
}
