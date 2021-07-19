using Shearlegs.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shearlegs.Web.Database
{
    public interface IUsersRepository
    {
        Task<MUser> AddUserAsync(MUser user);
        Task<MUserPlugin> AddUserPluginAsync(MUserPlugin userPlugin);
        Task DeleteUserPluginAsync(int userPluginId);
        Task<MUser> GetUserAsync(string name, string password);
        Task<MUser> GetUserAsync(int userId);
        Task<MUserPlugin> GetUserPluginAsync(int userPluginId);
        Task<IEnumerable<MUser>> GetUsersAsync();
        Task UpdateLastLoginDateAsync(int userId);
        Task<MUser> UpdateUserAsync(MUser user);
    }
}
