using ProductPhoneApis.Models;

namespace ProductPhoneApis.Business.AuthService.Interface
{
    public interface IAuthService
    {
        public Task<TokenReturn> Login(string Username, string password);
    }
}
