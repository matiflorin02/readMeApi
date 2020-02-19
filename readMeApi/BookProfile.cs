﻿using AutoMapper;
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
                .ReverseMap();
        }
    }
}
