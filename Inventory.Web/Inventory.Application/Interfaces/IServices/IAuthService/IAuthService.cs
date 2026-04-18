using Inventory.Application.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Application.Interfaces.IServices.IAuthService
{
    public interface IAuthService
    {
        Task<(bool Success, string Message)> RegisterAsync(RegisterViewModel model);
        Task<(bool Success, string Message)> LoginAsync(LoginViewModel model);
        Task LogoutAsync();
    }
}
