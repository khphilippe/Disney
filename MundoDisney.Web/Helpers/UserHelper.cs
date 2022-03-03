using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MundoDisney.Web.Data;
using MundoDisney.Web.Models;
using System.Threading.Tasks;

namespace MundoDisney.Web.Helpers
{
    public class UserHelper : IUserHelper
    {
        private readonly DataContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public UserHelper(DataContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IdentityResult> AddUserAsync(IdentityUser user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }

        public async Task<SignInResult> LoginAsync(LoginViewModel model)
        {
            return await _signInManager.PasswordSignInAsync(
                model.Username,
                model.Password,
                model.RememberMe,
                false);
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }


        public async Task<SignInResult> ValidatePasswordAsync(IdentityUser user, string password)
        {
            return await _signInManager.CheckPasswordSignInAsync(user, password, false);
        }


        public async Task<IdentityUser> GetUserAsync(string email)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser user)
        {
            return await _userManager.GenerateEmailConfirmationTokenAsync(user);
        }


        public async Task<IdentityResult> ConfirmEmailAsync(IdentityUser user, string token)
        {
            return await _userManager.ConfirmEmailAsync(user, token);
        }

        public async Task<IdentityUser> AddUserAsync(AddUserViewModel model)
        {
            IdentityUser user = new IdentityUser
            {

                Email = model.Username,
                UserName = model.Username,
                EmailConfirmed = true
            };

            IdentityResult result = await _userManager.CreateAsync(user, model.Password);
            if (result != IdentityResult.Success)
            {
                return null;
            }

            IdentityUser newUser = await GetUserAsync(model.Username);

            return newUser;
        }

    }
}
