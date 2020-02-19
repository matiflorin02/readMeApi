using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using readMe.Data.Repositories.Interfaces;
using readMe.Domain.Entities;
using readMe.Domain.Models;

namespace readMe.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BooksContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepository(BooksContext context, ILogger<UserRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<List<User>> GetAllUsers()
        {
            _logger.LogInformation("Getting a user list");
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUserByEmail(string email)
        {
            _logger.LogInformation($"Getting a user with the email {email}");
            return await _context.Users.FirstOrDefaultAsync(u => u.UserName == email);
        }

        public async Task<User> GetUserById(int id)
        {
            _logger.LogInformation($"Getting a user with the id {id}");
            return await _context.Users.FindAsync(id);
        }

        public  void AddNewUser(User user)
        {
            _logger.LogInformation($"Adding new user");
            _context.Users.Add(user);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Saving changes in the context");
            return (await _context.SaveChangesAsync()) > 0;
        }

        public void UpdateUserInfo(User user)
        {
            _context.Entry(user).State = EntityState.Modified;
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
