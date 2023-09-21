using StorageAppMvc.Data;
using System.ComponentModel.DataAnnotations;

namespace StorageAppMvc.Domain
{
    public class Room : IEntity
    {
        // Room properties //

        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Container>? Containers { get; set; } = new List<Container>(); // You can ask for Room.Containers to get all of the items.


        // Room specific methods //
        public void AddContainer(Container container)
        {

        }
        public void DeleteContainer(Container container)
        {

        }


        // IEntity methods to CRUD a room //
        private StorageDb context = new StorageDb();
        public void Create()
        {
            context.Add(this);
            context.SaveChanges();
        }

        public void Delete()
        {
            var roomToDelete = context.Rooms.FirstOrDefault(room => room.Id == Id);

            // We have to check for null incase it doesn't find anything.
            if (roomToDelete != null)
            {
                // Remove the room
                context.Remove(roomToDelete);
                context.SaveChanges();
            }
        }

        public void Update()
        {
            var roomToUpdate = context.Rooms.FirstOrDefault(room => room.Id == Id);

            if (roomToUpdate != null)
            {
                // Change it's (changable) values
                roomToUpdate.Name = Name;
                roomToUpdate.Containers = Containers;

                // Update the values
                context.Update(roomToUpdate);
                context.SaveChanges();
            }
        }

        // Constructors
        // This constructor is for when you first create a room to be added to the database.
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
