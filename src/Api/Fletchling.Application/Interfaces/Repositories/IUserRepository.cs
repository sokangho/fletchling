using System.Threading.Tasks;
using Fletchling.Domain.ApiModels.Requests;
using Fletchling.Domain.Entities;

namespace Fletchling.Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task AddUserAsync(AddUserRequest user);
        Task<User> GetUserAsync(string uid);
    }
}