using System.Collections.Generic;
using System.Threading.Tasks;
using readMe.Domain.Entities;

namespace readMe.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllUsers();
        User GetUserByEmail(string email);

        Task<User> GetUserById(int id);

        Task<User> SaveUser(UserCredentials userCredentials);

        Task<bool> UpdateUserInfo(int id, User user);
    }
}
