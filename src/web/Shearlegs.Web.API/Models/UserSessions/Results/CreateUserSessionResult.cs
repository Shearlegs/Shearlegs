using Shearlegs.Web.API.Utilities.StoredProcedures;
using System;

namespace Shearlegs.Web.API.Models.UserSessions.Results
{
    public class CreateUserSessionResult
    {
        public StoredProcedureResult StoredProcedureResult { get; set; }
        public Guid? SessionId { get; set; }
    }
}