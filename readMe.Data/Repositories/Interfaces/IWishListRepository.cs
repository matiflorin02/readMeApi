using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using readMe.Domain.Entities;

namespace readMe.Data.Repositories.Interfaces
{
    public interface IWishListRepository
    {
        void AddNewWishList(Wishlist wishList);

        Task<bool> SaveChangesAsync();

        Task<List<Wishlist>> GetAllWishLists();

        Task<Wishlist> GetWishListById(int id);

        Task<Wishlist> GetWishListByName(string name);

        void UpdateWishList(Wishlist wishList);

    }
}
