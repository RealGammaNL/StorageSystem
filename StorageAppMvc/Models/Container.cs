using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using StorageSystem.Data;

namespace StorageSystem.Models
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
        public List<Item>? Items { get; set; } = new List<Item>(); // You can ask for Container.Items to get all of the items.


        // Container specific methods //
        public void Move(Room currentRoom, Room targetRoom)
        {

        }

        public void AddItem(Item item)
        {

        }

        public void DeleteItem(Item item) 
        { 
        
        }



        // IEntity methods to CRUD a container //
        private StorageDb context = new StorageDb();
        public void Create()
        {
            context.Add(this);
            context.SaveChanges();
        }

        public void Delete()
        {
            var containerToDelete = context.Containers.FirstOrDefault(container => container.Id == Id);

            // We have to check for null incase it doesn't find anything.
            if (containerToDelete != null)
            {
                // Remove the container
                context.Remove(containerToDelete);
                context.SaveChanges();
            }
        }

        public void Update()
        {
            var containerToUpdate = context.Containers.FirstOrDefault(container => container.Id == Id);

            if (containerToUpdate != null)
            {
                // Change it's (changable) values
                containerToUpdate.Name = Name;
                containerToUpdate.Description = Description;
                containerToUpdate.Items = Items;

                // Update the values
                context.Update(containerToUpdate);
                context.SaveChanges();
            }
        }

        // Constructors
        // This constructor is for when you first create a container to be added to the database.
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
    }
}
