﻿namespace readMe.Domain.Models
{
    public class UserModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public WishListModel WishList { get; set; }
    }
}
