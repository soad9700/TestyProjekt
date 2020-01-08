using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DAL.Core.Interfaces;
using DAL.Models;

namespace DAL.Core
{
    public class UserManager : IUserManager
    {
        public Task<ApplicationUser> FindByIdAsync(string userName)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApplicationUser> FindByNameAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new Exception("Username cannot be null or empty");
            }
            var appUser = new ApplicationUser();
            appUser.UserName = "testowy";
            return Task<ApplicationUser>.Factory.StartNew(() => appUser);
        }

        public Task<ApplicationUser> FindByEmailAsync(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("Email cannot be null or empty");
            }
            var appUser = new ApplicationUser();
            appUser.Email = "test@test.com";
            
            return Task<ApplicationUser>.Factory.StartNew(() => appUser);
        }

        public Task<ApplicationUser> UpdateAsync(ApplicationUser email)
        {
            throw new NotImplementedException();
        }

        public Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> CreateAsync(string login, string password)
        {
            throw new System.NotImplementedException();
        }

        public Task<ApplicationRole> GetRoleByIdAsync(string roleId)
        {
            throw new NotImplementedException();
        }

        public Task<ApplicationRole> GetRoleByNameAsync(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}
