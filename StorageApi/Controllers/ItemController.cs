using Microsoft.AspNetCore.Mvc;
using StorageAppMvc.Domain;
using Microsoft.EntityFrameworkCore;
using StorageAppMvc.Data;

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
        [HttpGet]
        public IEnumerable<Item> Get()
        {
            return _context.Items.ToList();
        }

        // GET api/<ItemController>/5
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
        public void Post(string name, string desc)
        {
            Item item = new Item(name, desc);
            _context.Add(item);
            _context.SaveChanges();
        }

        // PUT api/<ItemController>/5
        [HttpPut("{id}")]
        public void Put(int id, string name, string desc)
        {
            Item itemToUpdate = _context.Items.FirstOrDefault(item => item.Id == id);

            if (itemToUpdate != null)
            {
                itemToUpdate.Name = name;
                itemToUpdate.Description = desc;

                _context.Update(itemToUpdate);
                _context.SaveChanges();
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
