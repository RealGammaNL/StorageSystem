using Microsoft.AspNetCore.Mvc;
using StorageAppMvc.Data;
using Domain;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StorageApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private readonly StorageDb _context;

        public RoomController(StorageDb context)
        {
            _context = context;
        }


        // GET: api/<RoomController>
        [HttpGet]
        public IEnumerable<Room> Get()
        {
            return _context.Rooms.ToList();
        }

        // GET api/<RoomController>/5
        [HttpGet("{id}")]
        public Room Get(int id)
        {
            Room Room = _context.Rooms.FirstOrDefault(Room => Room.Id == id);

            if (Room != null)
            {
                var containers = _context.Containers.Where(c => c.RoomId == id).ToList();

                if (containers.Any())
                {
                    Room.Containers = containers;
                }
                else
                {
                    Room.Containers = new List<Container>();
                }

                return Room;
            }

            else
            {
                return Room;
            }
        }

        ///// <summary>
        ///// Returns all rooms according to the roomnumber
        ///// </summary>
        ///// <param name="id">RoomId</param>
        ///// <returns>List of containers</returns>
        //[HttpGet("{id}")]
        ////[Route("api/Container/GetRoomContainers")]
        //public IActionResult GetRoomContainers(int id)
        //{
        //    var containers = _context.Containers.Where(c => c.RoomId == id).ToList();

        //    if (containers.Any())
        //    {
        //        return Ok(containers);
        //    }

        //    return BadRequest($"No containers where found for RoomID: {id}");
        //}

        // POST api/<RoomController>
        [HttpPost]
        public void Post(string name)
        {
            Room Room = new Room(name);
            _context.Add(Room);
            _context.SaveChanges();
        }

        // PUT api/<RoomController>/5
        [HttpPut("{id}")]
        public void Put(int id, string name)
        {
            Room RoomToUpdate = _context.Rooms.FirstOrDefault(Room => Room.Id == id);

            if (RoomToUpdate != null)
            {
                RoomToUpdate.Name = name;

                _context.Update(RoomToUpdate);
                _context.SaveChanges();
            }
        }
        
        // DELETE api/<RoomController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            Room RoomToDelete = _context.Rooms.FirstOrDefault(Room => Room.Id == id);

            if (RoomToDelete != null)
            {
                _context.Remove(RoomToDelete);
                _context.SaveChanges();
                return Ok();
            }

            return NotFound("That id isn't found, try again");
        }
    }
}
