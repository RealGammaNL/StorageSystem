using Microsoft.AspNetCore.Mvc;
using StorageAppMvc.Data;
using Domain;
using StorageAppMvc.Controllers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StorageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContainerController : ControllerBase
    {
        private readonly StorageDb _context;

        public ContainerController(StorageDb context)
        {
            _context = context;
        }


        // GET: api/<ContainerController>
        [HttpGet]
        public IEnumerable<Container> Get()
        {
            return _context.Containers.ToList();
        }

        // GET api/<ContainerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            Container Container = _context.Containers.FirstOrDefault(Container => Container.Id == id);

            if (Container != null)
            {
                return Ok(Container);
            }

            return NotFound("That id isn't found, try again");
        }

        // POST api/<ContainerController>
        [HttpPost]
        public void Post(string name, string desc)
        {
            Container Container = new Container(name, desc);
            _context.Add(Container);
            _context.SaveChanges();
        }

        // PUT api/<ContainerController>/5
        [HttpPut("{id}")]
        public void Put(int id, string? name, string? desc, int? roomid)
        {
            Container ContainerToUpdate = _context.Containers.FirstOrDefault(Container => Container.Id == id);

            if (ContainerToUpdate != null)
            {
                if (name != null)
                {
                    ContainerToUpdate.Name = name;
                }
                if (desc != null)
                {
                    ContainerToUpdate.Description = desc;
                }
                if (roomid != 0)
                {
                    ContainerToUpdate.RoomId = roomid;
                }

                _context.Update(ContainerToUpdate);
                _context.SaveChanges();
            }
        }

        // DELETE api/<ContainerController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Container ContainerToDelete = _context.Containers.FirstOrDefault(Container => Container.Id == id);

            if (ContainerToDelete != null)
            {
                _context.Remove(ContainerToDelete);
                _context.SaveChanges();
                return Ok();
            }

            return NotFound("That id isn't found, try again");
        }


    }
}
