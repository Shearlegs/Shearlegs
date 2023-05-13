using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Orchestrations.Schedulings
{
    public interface ISchedulingOrchestrationService
    {
        ValueTask<string> EnqueueAsync(Expression<Func<Task>> methodCall);
    }
}