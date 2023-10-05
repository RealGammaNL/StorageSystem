//using Microsoft.EntityFrameworkCore;
//using StorageAppMvc.Data;
using System.ComponentModel.DataAnnotations;

namespace Domain
{
    public class Room : IEntity
    {
        // Room properties //

        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public ICollection<Container>? Containers { get; set; } // You can ask for Room.Containers to get all of the items.

        //private readonly StorageDb _context;
        //public Room(StorageDb context)
        //{
        //    _context = context;
        //}

        // Room specific methods //
        public void AddContainer(Container container)
        {

        }
        public void DeleteContainer(Container container)
        {

        }


        // IEntity methods to CRUD a room //
        public void Create()
        {
            //_context.Add(this);
            //_context.SaveChanges();
        }

        public void Delete()
        {
            //var roomToDelete = _context.Rooms.FirstOrDefault(room => room.Id == Id);

            //// We have to check for null incase it doesn't find anything.
            //if (roomToDelete != null)
            //{
            //    // Remove the room
            //    _context.Remove(roomToDelete);
            //    _context.SaveChanges();
            //}
        }

        public void Update()
        {
            //var roomToUpdate = _context.Rooms.FirstOrDefault(room => room.Id == Id);

            //if (roomToUpdate != null)
            //{
            //    // Change it's (changable) values
            //    roomToUpdate.Name = Name;
            //    roomToUpdate.Containers = Containers;

            //    // Update the values
            //    _context.Update(roomToUpdate);
            //    _context.SaveChanges();
            //}
        }

        // Constructors
        // This constructor is for when you first create a room to be added to the database.
        public Room() { }

        public Room(string name)
        {
            Name = name;
        }

        // This constructor is for when want to access the room without knowing the containers in it.
        public Room(int id, string name)
        {
            Id = id;
            Name = name;
        }

        //This overloaded constructor is for when you load existing rooms from the database.
        public Room(int id, string name, List<Container> containers)
        {
            Id = id;
            Name = name;
            Containers = containers;
        }
    }
}
