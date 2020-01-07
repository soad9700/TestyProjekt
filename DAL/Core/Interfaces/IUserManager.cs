using DAL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Core.Interfaces
{
    public interface IUserManager
    {
        Task<ApplicationUser> FindByIdAsync(string userName);
    }
}
