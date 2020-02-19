using System.Collections.Generic;
using System.Threading.Tasks;
using readMe.Domain.Entities;

namespace readMe.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        void AddNewUser(User user);

        Task<bool> SaveChangesAsync();

        Task<List<User>> GetAllUsers();

        Task<User> GetUserByEmail(string email);

        Task<User> GetUserById(int id);

        void UpdateUserInfo(User user);
    }
}
