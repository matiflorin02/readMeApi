using System.Collections.Generic;

namespace readMe.Domain.Entities
{
    public class Wishlist
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string ListName { get; set; }

        public List<WishListBook> WishListBooks { get; set; }
    }
}