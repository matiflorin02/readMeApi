using System;
using System.Collections.Generic;
using System.Text;

namespace readMe.Domain.Models
{
    public class WishListBook
    {
        public int WishListId { get; set; }

        public int BookId { get; set; }

        public WishListModel WishList { get; set; }

        public BookModel Book { get; set; }
    }
}
