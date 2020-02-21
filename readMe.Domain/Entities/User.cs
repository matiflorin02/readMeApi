using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace readMe.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public List<Wishlist> WishListBook { get; set; }
    }
}
