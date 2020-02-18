using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using readMe.Domain.Entities;

namespace readMe.Data.Repositories.Interfaces
{
    public interface IBookRepository
    {
        void AddNewBook(Book entity);
        void Delete(Book entity);
        Task<bool> SaveChangesAsync();

        Task<List<Book>> GetBookList();

        Task<Book> GetBookById(int id);

        void UpdateBook(Book book);

        Task<Book> GetBookByIsbn(string isbn);
    }
}
