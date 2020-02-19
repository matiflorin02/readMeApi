using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using readMe.Domain.Entities;

namespace readMe.Data.Repositories.Interfaces
{
    public interface IWishListBookRepository
    {
        void AddNewEntry(WishListBook addedBooks);

        Task<bool> SaveChangesAsync();

        Task<List<WishListBook>> GetAllAddedBooks();

        Task<List<Wishlist>> GetAddedBooksForList(int userId);
    }
}
