namespace readMe.Domain.Entities
{
    public class Book
    {
        public int Id { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Category { get; set; }
        public int PageCount { get; set; }
        public string ImageSource { get; set; }
        public string Description { get; set; }
    }
}
