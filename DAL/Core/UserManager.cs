using DAL.Core.Interfaces;
using DAL.Models;
using System.Threading.Tasks;

namespace DAL.Core
{
    public class UserManager : IUserManager
    {
        public Task<ApplicationUser> FindByIdAsync(string userName)
        {
            throw new System.NotImplementedException();
        }
    }
}
