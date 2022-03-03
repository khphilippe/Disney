using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MundoDisney.Web.Data;
using MundoDisney.Web.Helpers;
using MundoDisney.Web.Models;
using MundoDisney.Web.Services;
using System.Linq;
using System.Threading.Tasks;

namespace MundoDisney.Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserHelper _userHelper;

        private readonly DataContext _context;

        private readonly IMailService _mailService;

        public AccountController(IUserHelper userHelper, DataContext context, IMailService mailService)
        {
            _userHelper = userHelper;
            _context = context;
            _mailService = mailService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Users

                .ToListAsync());
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                
                Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.LoginAsync(model);
                if (result.Succeeded)
                {
                    if (Request.Query.Keys.Contains("ReturnUrl"))
                    {
                        return Redirect(Request.Query["ReturnUrl"].First());
                    }

                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Email or password incorrect.");
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            await _userHelper.LogoutAsync();
            return RedirectToAction("Index", "Home");
        }


        public IActionResult Register()
        {
            AddUserViewModel model = new AddUserViewModel();
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AddUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userHelper.AddUserAsync(model);

                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "This email is already used.");

                    return View(user);
                }


                LoginViewModel loginViewModel = new LoginViewModel
                {
                    Password = model.Password,
                    RememberMe = false,
                    Username = model.Username
                };

                var result2 = await _userHelper.LoginAsync(loginViewModel);

                if (result2.Succeeded)
                {
                    await _mailService.SendMail(user.Email);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(model);

        }

    }


}



