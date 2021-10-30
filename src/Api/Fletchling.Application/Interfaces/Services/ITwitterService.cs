using System.Collections.Generic;
using System.Threading.Tasks;
using Fletchling.Domain.ApiModels;

namespace Fletchling.Application.Interfaces.Services
{
    public interface ITwitterService
    {
        Task<List<TwitterUser>> SearchUsersAsync(string username);
        Task<TwitterUser> GetUserAsync(long userId);
    }
}