using System.Threading.Tasks;
using MatrixHeroes_Api.Core.Models;
using MatrixHeroes_Api.Core.Models.Dtos;

namespace MatrixHeroes_Api.Core.Services
{
    public interface IAuthService
    {
        Task<AppResponse> SignUp(UserCredentials userCredentials);
        Task<AppResponse> SignIn(UserCredentials userCredentials);
    }
}