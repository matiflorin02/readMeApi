using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using readMe.Data;
using readMe.Data.Repositories.Interfaces;
using readMe.Domain.Entities;
using readMe.Domain.Models;

namespace readMeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ControllerBase
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BooksController(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        // GET: api/Books
        [HttpGet]
        public async Task<ActionResult<List<BookModel>>> GetBooks()
        {
            try
            {
                var configuration = new MapperConfiguration(cfg => cfg.CreateMap<Book, BookModel>());
                var booksResults = await _bookRepository.GetBookList();
                return _mapper.Map<List<Book>, List<BookModel>>(booksResults);
            }
            catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    ex);
            }
        }

        [HttpGet("{isbn}")]
        public async Task<ActionResult<BookModel>> GetBookByIsbn(string isbn)
        {
            try
            {
                var bookResult = await _bookRepository.GetBookByIsbn(isbn);
                return _mapper.Map<BookModel>(bookResult);
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    $"Recovering data for the book with id {isbn} failed");
            }
        }

        // PUT: api/Books/5
        [HttpPut("{isbn}")]
        public async Task<ActionResult<BookModel>> PutBook(string isbn, BookModel book)
        {
            try
            {
                var oldBook = await _bookRepository.GetBookByIsbn(isbn);
                if (oldBook == null)
                {
                    return NotFound($"The book with the isbn {isbn} was not found");
                }

                _mapper.Map(book, oldBook);

                _bookRepository.UpdateBook(oldBook);

                if (await _bookRepository.SaveChangesAsync())
                {
                    return _mapper.Map<BookModel>(oldBook);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    "Updating a book gave a database error");
            }

            return BadRequest();
        }

        // POST: api/Books
        [HttpPost]
        public async Task<ActionResult<BookModel>> PostBook(BookModel model)
        {
            try
            {
                var existingBook = await _bookRepository.GetBookByIsbn(model.ISBN);
                if (existingBook != null)
                {
                    BadRequest("Book already exists");
                }

                //create new book
                var newBook = _mapper.Map<Book>(model);
                _bookRepository.AddNewBook(newBook);
                if (await _bookRepository.SaveChangesAsync())
                {
                    return Created($"/api/books/{newBook.ISBN}", _mapper.Map<BookModel>(newBook));
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Adding the new book to the database failed");
            }

            return BadRequest();
        }

        // DELETE: api/Books/5
        [HttpDelete("{isbn}")]
        public async Task<ActionResult<Book>> DeleteBook(string isbn)
        {
            try
            {
                var book = await _bookRepository.GetBookByIsbn(isbn);
                if (book == null)
                {
                    return NotFound();
                }

                _bookRepository.Delete(book);
                if (await _bookRepository.SaveChangesAsync())
                {
                    return Ok();
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete book");
        }

    }
}
