using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using readMe.Data.Repositories.Interfaces;
using readMe.Domain.Entities;
using readMe.Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace readMeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WishListBooksController : ControllerBase
    {
        private readonly IWishListBookRepository _repository;
        private readonly IMapper _mapper;

        public WishListBooksController(IWishListBookRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/WishListBooks
        [HttpGet]
        public async Task<ActionResult<List<WishListBookModel>>> GetWishListBooks()
        {
            try
            {
                var wishListBooks = await _repository.GetAllAddedBooks();
                return _mapper.Map<List<WishListBookModel>>(wishListBooks);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Getting all failed");
            }
        }

        //// GET: api/WishListBooks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WishListBookModel>> GetWishListBook(int id)
        {
            try
            {
                var itemResult = await _repository.GetItemForId(id);
                return _mapper.Map<WishListBookModel>(itemResult);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Getting all failed");
            }
        }

        [HttpGet("userlist/{userId}")]
        public async Task<ActionResult<List<WishListModel>>> GetWishListForUser(int userId)
        {
            try
            {
                var itemResult = await _repository.GetAddedBooksForList(userId);
                return _mapper.Map<List<WishListModel>>(itemResult);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Getting all failed");
            }
        }

        // PUT: api/WishListBooks/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutWishListBook(int id, WishListBook wishListBook)
        //{
        //    if (id != wishListBook.WishlistId)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(wishListBook).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!WishListBookExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        // POST: api/WishListBooks
        [HttpPost]
        public async Task<ActionResult<WishListBookModel>> PostWishListBook(WishListBookModel wishListBook)
        {
            try
            {
                //var existingItem = await _repository.get
                var newItem = _mapper.Map<WishListBook>(wishListBook);
                _repository.AddNewEntry(newItem);
                if (await _repository.SaveChangesAsync())
                {
                   return CreatedAtAction("GetWishListBook", new { id = newItem.WishlistId }, wishListBook);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "post item");
            }

            return BadRequest();

        }

        // DELETE: api/WishListBooks/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<WishListBook>> DeleteWishListBook(int id)
        //{
        //    var wishListBook = await _context.WishListBooks.FindAsync(id);
        //    if (wishListBook == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.WishListBooks.Remove(wishListBook);
        //    await _context.SaveChangesAsync();

        //    return wishListBook;
        //}

        //private bool WishListBookExists(int id)
        //{
        //    return _context.WishListBooks.Any(e => e.WishlistId == id);
        //}
    }
}
