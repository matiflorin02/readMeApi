using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using readMe.Data.Repositories.Interfaces;
using readMe.Domain.Entities;

namespace readMe.Data.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BooksContext _booksContext;

        public BookRepository(BooksContext booksContext)
        {
            _booksContext = booksContext;
        }
        public async Task<List<Book>> GetBookList()
        {
            return await _booksContext.Books.ToListAsync();
        }

        public async Task<Book> GetBookById(int id)
        {
            return await _booksContext.Books.FindAsync(id);
        }

        public Task<bool> UpdateBook(int id, Book book)
        {
            throw new NotImplementedException();
        }

        public async Task<Book> AddNewBook(Book book)
        { 
            _booksContext.Books.Add(book);
            await _booksContext.SaveChangesAsync();
            return book;
        }

        private bool BookExists(int id)
        {
            return _booksContext.Books.Any(e => e.Id == id);
        }
    }
}
