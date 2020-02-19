using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using readMe.Data.Repositories.Interfaces;
using readMe.Domain.Entities;
using readMe.Domain.Models;

namespace readMeApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger _logger;
        private readonly IMapper _mapper;

        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger, IMapper mapper)
        {
            _userRepository = userRepository;
            _logger = logger;
            _mapper = mapper;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<List<UserModel>>> GetUsers()
        {
            _logger.LogInformation("In GET all users");
            try
            {
                var users = await _userRepository.GetAllUsers();
                return _mapper.Map<List<User>, List<UserModel>>(users);
            }
            catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    ex);
            }
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserModel>> GetUser(int id)
        {
            _logger.LogInformation("In GET user by ID");
            try
            {
                var user = await _userRepository.GetUserById(id);
                return _mapper.Map<UserModel>(user);
            }
            catch(Exception ex)
            {
                return this.StatusCode(StatusCodes.Status500InternalServerError,
                    ex);
            }
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public async Task<ActionResult<UserModel>> PutUser(int id, UserModel user)
        {
            _logger.LogInformation("In PUT user");
            try
            {
                var oldUser = await _userRepository.GetUserById(id);
                if (oldUser == null)
                {
                    return NotFound($"The user with the id {id} was not found");
                }

                _mapper.Map(user, oldUser);
                _userRepository.UpdateUserInfo(oldUser);

                if (await _userRepository.SaveChangesAsync())
                {
                    return _mapper.Map<UserModel>(oldUser);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Updating a user gave a database error");
            }

            return BadRequest();
        }

        // POST: api/Users
        [HttpPost("login")]
        public async Task<ActionResult<UserModel>> Login(UserModel userModel)
        {
            _logger.LogInformation("In POST user");
            try
            {
                var user = await _userRepository.GetUserByEmail(userModel.UserName);
                if (user == null)
                {
                    return NotFound($"User with email {userModel.UserName} was not found");
                }

                return user.Password == userModel.Password ? _mapper.Map<UserModel>(user) : null;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Login a user gave a database error");
            }
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserModel>> Register(UserModel userModel)
        {
            _logger.LogInformation("In POST register user");
            try
            {
                var user = _mapper.Map<User>(userModel);
                _userRepository.AddNewUser(user);
                if (await _userRepository.SaveChangesAsync())
                {
                    return NoContent();
                }

                return BadRequest();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Registering a user gave a database error");
            }

        }

        // DELETE: api/Users/5
        //[HttpDelete("{id}")]
        //public async Task<ActionResult<User>> DeleteUser(int id)
        //{
        //    var user = await _context.Users.FindAsync(id);
        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.Users.Remove(user);
        //    await _context.SaveChangesAsync();

        //    return user;
        //}

        //private bool UserExists(int id)
        //{
        //    return _context.Users.Any(e => e.Id == id);
        //}
    }
}
