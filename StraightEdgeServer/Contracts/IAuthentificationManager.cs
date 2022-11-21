using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.DTOs;

namespace Contracts
{
    public interface IAuthentificationManager
    {
        Task<bool> ValidateUser(UserForAuthenticationDto user);
        Task<string> CreateToken();
    }
}
