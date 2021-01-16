using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace ChatApp.Services
{
    public interface IIdentityService
    {
        Task<AuthenticateResult> RegisterAsync(string password, string username, string email, IFormFile avatar);
    }
}