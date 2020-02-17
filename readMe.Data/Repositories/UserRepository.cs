using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using readMe.Data.Repositories.Interfaces;
using readMe.Domain.Entities;
using readMe.Domain.Models;

namespace readMe.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BooksContext _context;

        public UserRepository(BooksContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public User GetUserByEmail(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == email);
            return user;
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.FindAsync(id);

        }

        public async Task<User> SaveUser(UserCredentials userCredentials)
        {
            var user = GetUserByEmail(userCredentials.UserName);
            if (user != null)
            {
                return null;
            }

            var newUser = new User
            {
                UserName = userCredentials.UserName,
                Password = userCredentials.Password
            };

            _context.Add(newUser);
            await _context.SaveChangesAsync();
            return newUser;

        }

        public async Task<bool> UpdateUserInfo(int id, User user)
        {

            if (id != user.Id)
            {
                return false;
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
