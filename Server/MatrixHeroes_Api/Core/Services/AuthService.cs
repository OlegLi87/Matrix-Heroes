using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using MatrixHeroes_Api.Core.Models;
using MatrixHeroes_Api.Core.Models.Domain;
using MatrixHeroes_Api.Core.Models.Dtos;
using MatrixHeroes_Api.Infastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace MatrixHeroes_Api.Core.Services
{
    public class AuthService : IAuthService
    {
        private UserManager<AppUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private AppSettings _appSettings;

        public AuthService(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IOptions<AppSettings> appSettings)
           => (_userManager, _roleManager, _appSettings) = (userManager, roleManager, appSettings.Value);

        public async Task<AppResponse> SignUp(UserCredentials credentials)
        {
            var role = await _roleManager.FindByNameAsync(credentials.Role);
            if (role == null)
            {
                var error = new IdentityError { Description = "Role name is invalid." };
                return createErrorResponse(StatusCodes.Status400BadRequest, error);
            }

            var user = new AppUser { UserName = credentials.UserName, Email = credentials.Email };
            var userResult = await _userManager.CreateAsync(user, credentials.Password);
            if (!userResult.Succeeded)
                return createErrorResponse(StatusCodes.Status400BadRequest, userResult.Errors.ToArray());

            var userToRoleResult = await _userManager.AddToRoleAsync(user, role.Name);
            if (!userToRoleResult.Succeeded)
                return createErrorResponse(StatusCodes.Status400BadRequest, userToRoleResult.Errors.ToArray());

            var jwtToken = createJwtToken(user, role.Name);

            return new AppResponse
            {
                StatusCode = StatusCodes.Status201Created,
                ResponsePayload = new ResponsePayload { ResponseObj = new { jwtToken } }
            };
        }

        public async Task<AppResponse> SignIn(UserCredentials credentials)
        {
            var user = await _userManager.FindByNameAsync(credentials.UserName);
            var isPasswordValid = await _userManager.CheckPasswordAsync(user, credentials.Password);
            if (!isPasswordValid)
            {
                var error = new IdentityError { Description = "Provided credentials are invalid." };
                return createErrorResponse(StatusCodes.Status400BadRequest, error);
            }

            var role = (await _userManager.GetRolesAsync(user)).First();
            var jwtToken = createJwtToken(user, role);

            return new AppResponse
            {
                StatusCode = StatusCodes.Status200OK,
                ResponsePayload = new ResponsePayload { ResponseObj = new { jwtToken } }
            };
        }

        private string createJwtToken(AppUser user, string role)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] secretBytes = Encoding.ASCII.GetBytes(_appSettings.JwtSecret);

            SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[] {
                   new Claim(ClaimTypes.Name,user.UserName),
                   new Claim(ClaimTypes.Email,user.Email),
                   new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                   new Claim(ClaimTypes.Role,role)
                }),
                Expires = DateTime.Now.AddDays(10),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(secretBytes), SecurityAlgorithms.HmacSha512)
            };

            var jwtToken = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(jwtToken);
        }

        private AppResponse createErrorResponse(int statusCode, params IdentityError[] errors)
        {
            return new AppResponse
            {
                StatusCode = statusCode,
                ResponsePayload = new ResponsePayload { Errors = errors.Select(e => e.Description) }
            };
        }
    }
}