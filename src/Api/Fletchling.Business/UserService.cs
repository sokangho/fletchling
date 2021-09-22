using Fletchling.Data.Models;
using Fletchling.Data.Repositories;
using System.Threading.Tasks;

namespace Fletchling.Business
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepo;

        public UserService(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepo.AddUserAsync(user);
        }
    }
}
