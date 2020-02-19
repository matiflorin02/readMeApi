using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using readMe.Data.Repositories.Interfaces;
using readMe.Domain.Entities;

namespace readMe.Data.Repositories
{
    public class WishListBookRepository : IWishListBookRepository
    {
        private readonly BooksContext _context;

        public WishListBookRepository(BooksContext context)
        {
            _context = context;
        }

        public void AddNewEntry(WishListBook wishListBook)
        {
            _context.WishListBooks.Add(wishListBook);
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public async Task<List<WishListBook>> GetAllAddedBooks()
        {
            return await _context.WishListBooks.ToListAsync();
        }

        public async Task<List<Wishlist>> GetAddedBooksForList(int userId)
        {
           return await _context.Wishlists.Where(x=>x.UserId == userId).Include(w => w.WishListBooks).ThenInclude(wb => wb.Book).ToListAsync();
        }
    }
}
