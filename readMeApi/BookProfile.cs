using System.Linq;
using AutoMapper;
using readMe.Domain.Entities;
using readMe.Domain.Models;

namespace readMeApi
{
    public class BookProfile: Profile
    {
        public BookProfile()
        {
            this.CreateMap<Book, BookModel>()
                .ReverseMap();

            this.CreateMap<User, UserModel>()
                .ReverseMap();

            this.CreateMap<Wishlist, WishListModel>()
                .ForMember(wish => wish.Books, opt=> opt.MapFrom(src=> src.WishListBooks.Select(s=>s.Book)))
                .ReverseMap();

            this.CreateMap<WishListBook, WishListBookModel>()
                .ReverseMap();
        }
    }
}
