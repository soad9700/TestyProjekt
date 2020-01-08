// =============================
// Email: info@ebenmonney.com
// www.ebenmonney.com/templates
// =============================

using DAL.Core.Interfaces;
using DAL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core
{
    public class AccountManager : IAccountManager
    {
        private readonly IUserManager _userManager;


        public AccountManager(
            IUserManager userManager)
        {
            _userManager = userManager;
        }
        

        public async Task<ApplicationUser> GetUserByIdAsync(string userId)
        {
            return await _userManager.FindByIdAsync(userId);
        }

        public async Task<ApplicationUser> GetUserByUserNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IList<string>> GetUserRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
        

        public async Task<bool> CreateUserAsync(string login, string password)
        {
            var result = await _userManager.CreateAsync(login, password);
            
            if (string.IsNullOrEmpty(login))
                throw new ArgumentException("login cannot be null or empty", login);

            if (string.IsNullOrEmpty(login))
                throw new ArgumentException("login cannot be null or empty", password);

            if (password.Length < 8)
            {
                return false;
            }
            
            return true;
        }
        

        public async Task<ApplicationRole> GetRoleByIdAsync(string roleId)
        {
            return await _userManager.GetRoleByIdAsync(roleId);
        }


        public async Task<ApplicationRole> GetRoleByNameAsync(string roleName)
        {
            return await _userManager.GetRoleByNameAsync(roleName);
        }

        public async Task<(bool Succeeded, string[] Errors)> CreateRoleAsync(ApplicationRole role, IEnumerable<string> claims)
        {
            if (claims == null)
                claims = new string[] { };

            string[] invalidClaims = claims.Where(c => ApplicationPermissions.GetPermissionByValue(c) == null).ToArray();
            if (invalidClaims.Any())
                return (false, new[] { "The following claim types are invalid: " + string.Join(", ", invalidClaims) });

            
            return (true, new string[] { });
        }

        public async Task<(bool Succeeded, string[] Errors)> UpdateRoleAsync(ApplicationRole role1, IEnumerable<string> claims)
        {
            if (claims != null)
            {
                string[] invalidClaims = claims.Where(c => ApplicationPermissions.GetPermissionByValue(c) == null).ToArray();
                if (invalidClaims.Any())
                    return (false, new[] { "The following claim types are invalid: " + string.Join(", ", invalidClaims) });
            }

            var role = new ApplicationUser();
            var result = await _userManager.UpdateAsync(role);
            

            return (true, new string[] { });
        }
        
    }
}
