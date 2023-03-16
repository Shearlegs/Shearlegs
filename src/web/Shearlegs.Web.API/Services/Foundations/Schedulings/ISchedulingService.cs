using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Services.Foundations.Schedulings
{
    public interface ISchedulingService
    {
        ValueTask<string> EnqueueAsync(Expression<Func<Task>> methodCall);
    }
}