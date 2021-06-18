using Fletchling.Data.Models;
using System.Threading.Tasks;

namespace Fletchling.Data.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(User user);
        Task<User> GetUserAsync(string uid);
    }
}
