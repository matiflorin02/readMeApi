namespace readMe.Domain.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public WishListModel WishList { get; set; }
    }
}
