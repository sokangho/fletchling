using System.Threading.Tasks;
using Fletchling.Application.Interfaces.Repositories;
using Fletchling.Application.Interfaces.Services;
using Fletchling.Domain.ApiModels.Requests;

namespace Fletchling.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task AddUserAsync(AddUserRequest user)
        {
            await _userRepo.AddUserAsync(user);
        }
    }
}