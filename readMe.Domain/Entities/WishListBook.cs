namespace readMe.Domain.Entities
{
    public class WishListBook
    {
        public int WishlistId { get; set; }

        public int BookId { get; set; }

        public Wishlist Wishlist { get; set; }

        public Book Book { get; set; }

    }
}
