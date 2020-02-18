using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using readMe.Data.Repositories.Interfaces;
using readMe.Domain.Entities;

namespace readMe.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksContext _booksContext;
        private readonly ILogger<BookRepository> _logger;

        public BookRepository(BooksContext booksContext, ILogger<BookRepository> logger)
        {
            _booksContext = booksContext;
            _logger = logger;
        }

        public void AddNewBook(Book entity)
        {
            _logger.LogInformation($"Adding a new book to the context");
            _booksContext.Books.Add(entity);
        }

        public void Delete(Book entity)
        {
            _logger.LogInformation($"Deleting book with id {entity.Id}");
            _booksContext.Remove(entity);
        }

        public async Task<bool> SaveChangesAsync()
        {
            _logger.LogInformation($"Saving changes in the context");
            return (await _booksContext.SaveChangesAsync()) > 0;
        }

        public async Task<List<Book>> GetBookList()
        {
            _logger.LogInformation($"Getting all the books");
            return await _booksContext.Books.ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            _logger.LogInformation($"Getting the book with the id {id}");
            return await _booksContext.Books.FindAsync(id);
        }

        public void UpdateBook(Book book)
        {
            _booksContext.Entry(book).State = EntityState.Modified;
        }

        public async Task<Book> GetBookByIsbn(string isbn)
        {
            _logger.LogInformation($"Getting the book with the isbn {isbn}");
            return await _booksContext.Books.FirstOrDefaultAsync(b => b.ISBN == isbn);
        }


        private bool BookExists(int id)
        {
            return _booksContext.Books.Any(e => e.Id == id);
        }
    }
}
