using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using readMe.Domain.Entities;

namespace readMe.Data.Repositories.Interfaces
{
    public interface IBookRepository
    {
        Task<List<Book>> GetBookList();

        Task<Book> GetBookById(int id);

        Task<bool> UpdateBook(int id, Book book);

        Task<Book> AddNewBook(Book book);


    }
}
