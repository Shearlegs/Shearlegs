using Shearlegs.Web.API.Models.UserSessions;
using Shearlegs.Web.API.Models.UserSessions.Params;
using Shearlegs.Web.API.Models.UserSessions.Results;
using Shearlegs.Web.API.Utilities.StoredProcedures;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Shearlegs.Web.API.Brokers.Storages
{
    public partial interface IStorageBroker
    {
        ValueTask<CreateUserSessionResult> CreateUserSessionAsync(CreateUserSessionParams @params);
        ValueTask<UserSession> GetUserSessionAsync(GetUserSessionsParams @params);
        ValueTask<IEnumerable<UserSession>> GetUserSessionsAsync(GetUserSessionsParams @params);
        ValueTask<StoredProcedureResult> RevokeUserSessionAsync(RevokeUserSessionParams @params);
    }
}
