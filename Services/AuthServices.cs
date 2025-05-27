using Api.Dto;
using Api.Interfaces;
using Api.Mapping;
using Microsoft.AspNetCore.Identity;

namespace Api.Services
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AuthServices(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<bool> CheckExist(string email)
        {
            return await _userManager.FindByEmailAsync(email).ConfigureAwait(false) is not null;
        }

        public async Task<ServicesResult<string>> DeleteUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
                return ServicesResult<string>.Fail("UserNotFound", "User with the provided email does not exist.");

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                var errorMessages = string.Join("; ", result.Errors.Select(e => e.Description));
                return ServicesResult<string>.Fail("DeleteFailed", errorMessages);
            }

            return ServicesResult<string>.Ok(email, "User deleted successfully.");
        }


        public async Task<User> Login(LoginReq loginReq, CancellationToken cancellationToken)
        {
            User user = await _userManager.FindByEmailAsync(loginReq.email).ConfigureAwait(false);

            if (user is null) return null;

            var result = await _signInManager.CheckPasswordSignInAsync(user, loginReq.passWord, false).ConfigureAwait(false);

            if (!result.Succeeded) return null;

            return user;
        }

        public async Task<IdentityResult> Register(UserRequest userRequest)
        {
            User user = userRequest.ToUser();

            return await _userManager.CreateAsync(user, userRequest.password).ConfigureAwait(false);
        }
    }
}