using Lab05_Ederson_Vilca.Models;

namespace Lab05_Ederson_Vilca.Services
{
    public interface IAuthService
    {
        Task<string?> LoginAsync(LoginModel model);
        Task<Usuario> RegisterAsync(RegisterModel model);
    }
}