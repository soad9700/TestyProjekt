using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Models;

namespace DAL.Core.Interfaces
{
    public interface IUserManager
    {
        Task<ApplicationUser> FindByIdAsync(string userName);
        Task<ApplicationUser> FindByNameAsync(string userName);
        Task<ApplicationUser> FindByEmailAsync(string email);
        Task<ApplicationUser> UpdateAsync(ApplicationUser email);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<bool> CreateAsync(string login, string password);
        Task<ApplicationRole> GetRoleByIdAsync(string roleId);
        Task<ApplicationRole> GetRoleByNameAsync(string roleName);
    }
}