using Hangfire;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Schedulings
{
    public class SchedulingBroker : ISchedulingBroker
    {
        public ValueTask<string> EnqueueAsync(Expression<Func<Task>> methodCall)
        {
            string jobId = BackgroundJob.Enqueue(methodCall);

            return ValueTask.FromResult(jobId);
        }
    }
}
