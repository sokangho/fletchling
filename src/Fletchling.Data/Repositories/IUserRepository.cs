using Fletchling.Data.Models;
using System.Threading.Tasks;

namespace Fletchling.Data.Repositories
{
    public interface IUserRepository
    {
        Task AddUser(TwitterUserCredentials credentials);
    }
}
