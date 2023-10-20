using Domain;
using Domain.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StorageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly StorageDb _context;

        public UserController(StorageDb context)
        {
            _context = context;
        }
        // GET: api/<UserController>
        /// <summary>
        /// Return all Users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<User>> Get()
        {
            return await _context.Users.ToListAsync();
        }

        // GET api/<UserController>/5
        /// <summary>
        /// Return the User based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            User User = _context.Users.FirstOrDefault(User => User.Id == id);

            if (User != null)
            {
                return Ok(User);
            }

            return NotFound("That id isn't found, try again");
        }

        // POST api/<UserController>
        [HttpPost]
        public async Task<IActionResult> Post(User User)
        {
            try
            {
                if (User == null)
                {
                    return BadRequest("Invalid User data");
                }

                // Add the User to the context and save changes
                _context.Add(User);
                await _context.SaveChangesAsync();

                return Ok("User created successfully");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during processing
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // PUT api/<UserController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(User User)
        {
            try
            {
                if (User == null)
                {
                    return BadRequest("Invalid User data");
                }



                // Update the User
                User UserToEdit = _context.Users.First(UserToEdit => UserToEdit.Id == User.Id);
                UserToEdit.Name = User.Name;
                UserToEdit.Email = User.Email;
                UserToEdit.Password = User.Password;
                UserToEdit.Rooms = User.Rooms;

                _context.Update(UserToEdit);
                await _context.SaveChangesAsync();

                return Ok("User created successfully");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during processing
                return BadRequest($"Error: {ex.Message}");
            }

        }

        // DELETE api/<UserController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            User UserToDelete = _context.Users.FirstOrDefault(User => User.Id == id);

            if (UserToDelete != null)
            {
                _context.Remove(UserToDelete);
                _context.SaveChanges();
                return Ok();
            }

            return NotFound("That id isn't found, try again");
        }
    }
}
