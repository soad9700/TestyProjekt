// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core.Interfaces
{
    public interface IAccountManager
    {
        Task<bool> CreateUserAsync(string user, string password);
        Task<ApplicationRole> GetRoleByIdAsync(string roleId);
        Task<ApplicationRole> GetRoleByNameAsync(string roleName);
        Task<ApplicationUser> GetUserByEmailAsync(string email);
        Task<ApplicationUser> GetUserByIdAsync(string userId);
        Task<ApplicationUser> GetUserByUserNameAsync(string userName);
    }
}
