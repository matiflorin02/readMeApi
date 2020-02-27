using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using readMe.Data.Repositories.Interfaces;
using readMe.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace readMe.Data.Repositories
{
    public class WishListRepository : IWishListRepository
    {
        private readonly BooksContext _context;
        private readonly ILogger<WishListRepository> _logger;

        public WishListRepository(BooksContext context, ILogger<WishListRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddNewWishList(Wishlist wishList)
        {
            _logger.LogInformation("Add new wish list");
            _context.Wishlists.Add(wishList);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation("Saving changes");
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<Wishlist>> GetAllWishLists()
        {
            _logger.LogInformation("Get all wish lists");
            return await _context.Wishlists.ToListAsync();
        }

        public async Task<Wishlist> GetWishListById(int id)
        {
            _logger.LogInformation("Get wish list by id");
            return await _context.Wishlists.FindAsync(id);
        }

        public async Task<Wishlist> GetWishListByName(string name)
        {
            _logger.LogInformation("Get wish list by name");
            return await _context.Wishlists.FirstOrDefaultAsync(w => w.ListName == name);
        }

        public void UpdateWishList(Wishlist wishList)
        {
            _context.Entry(wishList).State = EntityState.Modified;
        }

        public async Task<List<Wishlist>> GetWishListsForUser(int userId)
        {
            return await _context.Wishlists.Where(x => x.UserId == userId).ToListAsync();
        }
    }
}
