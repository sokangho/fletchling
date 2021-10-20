using Fletchling.Data.Models;
using System.Threading.Tasks;

namespace Fletchling.Business.Contracts
{
    public interface IUserService
    {
        Task AddUserAsync(User user);
    }
}
