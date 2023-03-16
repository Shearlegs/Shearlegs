using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Schedulings
{
    public interface ISchedulingBroker
    {
        ValueTask<string> EnqueueAsync(Expression<Func<Task>> methodCall);
    }
}