using Microsoft.EntityFrameworkCore;
using readMe.Domain;
using readMe.Domain.Entities;

namespace readMe.Data
{
    public class BooksContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<Wishlist> Wishlists { get; set; }

        public BooksContext(DbContextOptions contextOptions) : base(contextOptions)
        {
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AddedBooks>().HasKey(s => new {s.WishlistId, s.BookId});
        }
    }
}
