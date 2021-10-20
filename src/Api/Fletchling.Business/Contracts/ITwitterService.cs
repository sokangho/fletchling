using Fletchling.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fletchling.Business.Contracts
{
    public interface ITwitterService
    {
        Task<List<TwitterUser>> SearchUsersAsync(string username);
        Task<TwitterUser> GetUserAsync(long userId);
    }
}
