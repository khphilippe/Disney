using Microsoft.AspNetCore.Identity;
using MundoDisney.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Web.Helpers
{
   public  interface IUserHelper
    {
        Task<IdentityResult> AddUserAsync(IdentityUser user, string password);

        Task<SignInResult> LoginAsync(LoginViewModel model);

        Task LogoutAsync();

        Task<IdentityUser> GetUserAsync(string email);

        Task<SignInResult> ValidatePasswordAsync(IdentityUser user, string password);


        Task<string> GenerateEmailConfirmationTokenAsync(IdentityUser user);

        Task<IdentityResult> ConfirmEmailAsync(IdentityUser user, string token);

        Task<IdentityUser> AddUserAsync(AddUserViewModel model);
    }
}
