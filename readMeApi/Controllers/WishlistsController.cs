using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
    public class WishlistsController : ControllerBase
    {
        private readonly IWishListRepository _wishListRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<WishlistsController> _logger;

        public WishlistsController(IWishListRepository wishListRepository, IMapper mapper, ILogger<WishlistsController> logger)
        {
            _wishListRepository = wishListRepository;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Wishlists
        [HttpGet]
        public async Task<ActionResult<List<WishListModel>>> GetWishlists()
        {
            try
            {
                var wishlists = await _wishListRepository.GetAllWishLists();

                return _mapper.Map<List<WishListModel>>(wishlists);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Recovering data for wishlists failed ");
            }
        }

        // GET: api/Wishlists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WishListModel>> GetWishlist(int id)
        {
            try
            {
                var wishlist = await _wishListRepository.GetWishListById(id);
                return _mapper.Map<WishListModel>(wishlist);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    $"Recovering data for the wishlist with id {id} failed");
            }
        }

        // PUT: api/Wishlists/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<ActionResult<WishListModel>> PutWishlist(int id, WishListModel wishlist)
        {
            try
            {
                var oldWishlist = await _wishListRepository.GetWishListById(id);
                _mapper.Map(wishlist, oldWishlist);
                _wishListRepository.UpdateWishList(oldWishlist);
                if (await _wishListRepository.SaveChangesAsync())
                {
                    return _mapper.Map<WishListModel>(oldWishlist);
                }
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Updating a wishlist gave a database error");
            }

            return BadRequest();
        }

        // POST: api/Wishlists
        [HttpPost]
        public async Task<ActionResult<WishListModel>> PostWishlist(WishListModel wishlist)
        {
            try
            {
                var existingList = await _wishListRepository.GetWishListByName(wishlist.ListName);
                if (existingList != null)
                {
                    return BadRequest("List already exists");
                }

                var newList = _mapper.Map<Wishlist>(wishlist);
                _wishListRepository.AddNewWishList(newList);
                if (await _wishListRepository.SaveChangesAsync())
                {
                    return CreatedAtAction("GetWishlists", new { id = newList.Id }, newList);
                }
            }
            catch (Exception)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError, "Adding the new list to the database failed");
            }

            return BadRequest();

        }

        // DELETE: api/Wishlists/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<Wishlist>> DeleteWishlist(int id)
        //{
        //    var wishlist = await _context.Wishlists.FindAsync(id);
        //    if (wishlist == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Wishlists.Remove(wishlist);
        //    await _context.SaveChangesAsync();

        //    return wishlist;
        //}

        //private bool WishlistExists(int id)
        //{
        //    return _context.Wishlists.Any(e => e.Id == id);
        //}
    }
}
