using Microsoft.AspNetCore.Mvc;
using Domain;
using Microsoft.EntityFrameworkCore;
using Domain.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StorageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly StorageDb _context;

        public ItemController(StorageDb context)
        {
            _context = context;
        }


        // GET: api/<ItemController>
        /// <summary>
        /// Return all items
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IEnumerable<Item>> Get()
        {
            return await _context.Items.ToListAsync();
        }

        // GET api/<ItemController>/5
        /// <summary>
        /// Return the item based on the id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Item item = _context.Items.FirstOrDefault(item => item.Id == id);

            if (item != null)
            {
                return Ok(item);
            }

            return NotFound("That id isn't found, try again");
        }

        // POST api/<ItemController>
        [HttpPost]
        public async Task<IActionResult> Post(Item item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Invalid item data");
                }

                // Add the item to the context and save changes
                _context.Add(item);
                await _context.SaveChangesAsync();

                return Ok("Item created successfully");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during processing
                return BadRequest($"Error: {ex.Message}");
            }
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(Item item)
        {
            try
            {
                if (item == null)
                {
                    return BadRequest("Invalid item data");
                }

                // Update the item
                Item itemToEdit = _context.Items.First(itemToEdit => itemToEdit.Id == item.Id);
                itemToEdit.Name = item.Name;
                itemToEdit.Description = item.Description;
                itemToEdit.Quantity = item.Quantity;

                _context.Update(itemToEdit);
                await _context.SaveChangesAsync();

                return Ok("Item created successfully");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during processing
                return BadRequest($"Error: {ex.Message}");
            }

        }

        // DELETE api/<ItemController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Item itemToDelete = _context.Items.FirstOrDefault(item => item.Id == id);

            if (itemToDelete != null )
            {
                _context.Remove(itemToDelete);
                _context.SaveChanges();
                return Ok();
            }

            return NotFound("That id isn't found, try again");
        }
    }
}
