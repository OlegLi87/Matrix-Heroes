using System.Threading.Tasks;
using MatrixHeroes_Api.Core.Models;
using MatrixHeroes_Api.Core.Models.Dtos;
using MatrixHeroes_Api.Core.Services;
using MatrixHeroes_Api.Infastructure.Filters;
using Microsoft.AspNetCore.Mvc;

namespace MatrixHeroes_Api.Controllers
{
    [ApiController]
    [Route("api/{controller}")]
    public class AccountController : ControllerBase
    {
        private IAuthService _authService;
        public AccountController(IAuthService authService) => _authService = authService;

        [HttpPost]
        [Route("signup")]
        [SignUpActionFilter(ActionArgumentName = "credentials")]
        public async Task<AppResponse> SignUp([FromBody] UserCredentials credentials)
        {
            return await _authService.SignUp(credentials);
        }

        [HttpPost]
        [Route("signin")]
        public async Task<AppResponse> SignIn([FromBody] UserCredentials credentials)
        {
            return await _authService.SignIn(credentials);
        }
    }
}