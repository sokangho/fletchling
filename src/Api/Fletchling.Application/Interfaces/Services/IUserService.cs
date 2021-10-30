using System.Threading.Tasks;
using Fletchling.Domain.ApiModels.Requests;

namespace Fletchling.Application.Interfaces.Services
{
    public interface IUserService
    {
        Task AddUserAsync(AddUserRequest user);
    }
}