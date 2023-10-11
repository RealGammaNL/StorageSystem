using System.ComponentModel.DataAnnotations;
//using Microsoft.EntityFrameworkCore;
//using Domain.Data;

namespace Domain
{
    public class Container : IEntity
    {
        // Container Properties //
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string? Description { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.Now;
        //public Image image { get; set; } ???

        public ICollection<Item>? Items { get; set; } = new List<Item>(); // You can ask for Container.Items to get all of the items.
        public int? RoomId { get; set; }

        //Doesnt work because of cycle references in API controller
        //public Room Room { get; set; }

        //private readonly StorageDb? _context;

        //public Item(StorageDb context)
        //{
        //    _context = context;
        //}

        // Constructors
        // This constructor is for when you first create a container to be added to the database.
        public Container() { }

        public Container(string name, string? description)
        {
            Name = name;
            Description = description;
        }

        // This overloaded constructor is for when you load existing containers from the database.
        public Container(int id, string name, string? description, List<Item> items, DateTime dateTime)
        {
            Id = id;
            Name = name;
            Description = description;
            AddedOn = dateTime;
            Items = items;
        }

        // Container specific methods //
        public void Move(Room currentRoom, Room targetRoom)
        {

        }

        //public void AddItem(Item item)
        //{
        //    Items.Add(item);
        //    _context.Update(this);
        //    _context.SaveChanges();
        //}

        //public void DeleteItem(Item item)
        //{
        //    var tempItem = Items.First(i => i.Id == item.Id);
        //    if (tempItem != null)
        //    {
        //        Items.Remove(tempItem);
        //        _context.Update(this);
        //        _context.SaveChanges();
        //    }
        //}



        //// IEntity methods to CRUD a container //
        public void Create()
        {
            //    _context.Add(this);
            //    _context.SaveChanges();
        }

        public void Delete()
        {
            //    var containerToDelete = _context.Containers.FirstOrDefault(container => container.Id == Id);

            //    // We have to check for null incase it doesn't find anything.
            //    if (containerToDelete != null)
            //    {
            //        // Remove the container
            //        _context.Remove(containerToDelete);
            //        _context.SaveChanges();
            //    }
        }

        public void Update()
        {
            //    var containerToUpdate = _context.Containers.FirstOrDefault(container => container.Id == Id);

            //    if (containerToUpdate != null)
            //    {
            //        // Change it's (changable) values
            //        containerToUpdate.Name = Name;
            //        containerToUpdate.Description = Description;
            //        containerToUpdate.Items = Items;

            //        // Update the values
            //        _context.Update(containerToUpdate);
            //        _context.SaveChanges();
            //    }
        }
    }
}
