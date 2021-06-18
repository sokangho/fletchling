using Fletchling.Twitter.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fletchling.Twitter.Services
{
    public interface ITwitterService
    {
        Task<List<User>> SearchUsersAsync(string username);
    }
}
