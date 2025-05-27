




using Api.Dto;
using Microsoft.AspNetCore.Identity;

namespace Api.Interfaces
{
    public interface IAuthServices
    {
        public Task<IdentityResult> Register(UserRequest user);

        public Task<User?> Login(LoginReq loginReq, CancellationToken cancellationToken);

        public Task<ServicesResult<string>> DeleteUser(string email);
        public Task<bool> CheckExist(string email);

    }
}