using ChatApp.Domain;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string password, string username, string email);
        Task<AuthenticationResult> LoginAsync(  string email, string password);
        Task<AuthenticationResult> RefreshTokenAsync(string jwtToken,string refreshToken);
       

    }
}