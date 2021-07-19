using Shearlegs.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Database.Repositories
{
    public class WindowsUsersRepository : UsersRepository
    {
        public WindowsUsersRepository(SqlConnection connection) : base(connection)
        {

        }

        public override Task<MUser> GetUserAsync(string name, string password)
        {
            throw new NotImplementedException();
        }


    }
}
