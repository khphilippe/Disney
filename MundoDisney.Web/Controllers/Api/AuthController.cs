using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MundoDisney.Commonn.Request;
using MundoDisney.Web.Helpers;
using MundoDisney.Web.Models;
using MundoDisney.Web.Services;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MundoDisney.Web.Controllers.Api
{
    [Route("/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        private readonly IUserHelper _userHelper;
        private readonly IConfiguration _configuration;
        private readonly IMailService _mailService;




        public AuthController(IUserHelper userHelper, IConfiguration configuration, IMailService mailService)
        {
            _userHelper = userHelper;
            _configuration = configuration;
            _mailService = mailService;
        }






        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityUser user = await _userHelper.GetUserAsync(model.Username);
                if (user != null)
                {
                    Microsoft.AspNetCore.Identity.SignInResult result = await _userHelper.ValidatePasswordAsync(user, model.Password);

                    if (result.Succeeded)
                    {
                        Claim[] claims = new[]
                        {
                        new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };

                        SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Tokens:Key"]));
                        SigningCredentials credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        JwtSecurityToken token = new JwtSecurityToken(
                            _configuration["Tokens:Issuer"],
                            _configuration["Tokens:Audience"],
                            claims,
                            expires: DateTime.UtcNow.AddDays(99),
                            signingCredentials: credentials);
                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo,
                            user
                        };

                        return Created(string.Empty, results);
                    }
                }
            }

            return BadRequest();
        }





        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> PostUser([FromBody] UserRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Commonn.Responses.Response
                {
                    IsSuccess = false,
                    Message = "Bad request",
                    Result = ModelState
                });
            }

            IdentityUser user = new IdentityUser
            {
                Email = request.Email,
                UserName = request.Email,
                EmailConfirmed = true

            };

            IdentityResult result = await _userHelper.AddUserAsync(user, request.Password);
            if (result != IdentityResult.Success)
            {
                return BadRequest(result.Errors.FirstOrDefault().Description);
            }

            await _mailService.SendMail(user.Email);

            return Ok(new Commonn.Responses.Response
            {
                Message = "The user with " + user.Email + " has been added successfully ",
                Result = Response.StatusCode,
                IsSuccess = true
            });

        }





        [HttpGet]
        [Route("mail")]

        public async Task<IActionResult> SendMailToUser()
        {
            await _mailService.SendMail("kevensherbyson@gmail.com");



            return Ok();
        }

    }
}
