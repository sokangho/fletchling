using Fletchling.Twitter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fletchling.Twitter.Services
{
    public interface ITwitterService
    {
        Task<List<TwitterUser>> SearchUsersAsync(string username);
        Task<TwitterUser> GetUserAsync(long userId);
    }
}
