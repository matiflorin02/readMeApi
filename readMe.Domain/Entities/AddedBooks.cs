namespace readMe.Domain.Entities
{
    public class AddedBooks
    {
        public int WishlistId { get; set; }

        public int BookId { get; set; }

        public Wishlist Wishlist { get; set; }

        public Book Book { get; set; }

    }
}
