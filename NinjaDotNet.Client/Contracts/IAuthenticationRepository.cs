using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NinjaDotNet.Client.Models;

namespace NinjaDotNet.Client.Contracts
{
    public interface IAuthenticationRepository
    {
        Task<bool> Register(RegistrationModel user);
        Task<bool> Login(LoginModel user);
        public Task Logout();
    }
}
