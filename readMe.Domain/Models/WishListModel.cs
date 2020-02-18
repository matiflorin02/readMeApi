using System;
using System.Collections.Generic;
using System.Text;

namespace readMe.Domain.Models
{
    public class WishListModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string ListName { get; set; }
    }
}
