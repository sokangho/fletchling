using Fletchling.Data.Models;
using System.Threading.Tasks;

namespace Fletchling.Business
{
    public interface IUserService
    {
        Task AddUserAsync(User user);
    }
}
