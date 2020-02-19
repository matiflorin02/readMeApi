namespace readMe.Domain.Entities
{
    public class Wishlist
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public string ListName { get; set; }

        public WishListBook WishListBooks { get; set; }
    }
}