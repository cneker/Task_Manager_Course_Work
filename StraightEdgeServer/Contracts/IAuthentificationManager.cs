using Entities.DTOs;

namespace Contracts
{
    public interface IAuthentificationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto user);
        Task<string> CreateToken();
    }
}
