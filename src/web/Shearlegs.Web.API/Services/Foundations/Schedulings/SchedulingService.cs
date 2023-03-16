using Shearlegs.Web.API.Brokers.Schedulings;
using System.Linq.Expressions;
using System;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.Schedulings
{
    public class SchedulingService : ISchedulingService
    {
        private readonly ISchedulingBroker schedulingBroker;

        public SchedulingService(ISchedulingBroker schedulingBroker)
        {
            this.schedulingBroker = schedulingBroker;
        }

        public async ValueTask<string> EnqueueAsync(Expression<Func<Task>> methodCall)
        {
            string jobId = await schedulingBroker.EnqueueAsync(methodCall);

            return jobId;
        }
    }
}
