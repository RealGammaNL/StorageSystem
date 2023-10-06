//using Microsoft.EntityFrameworkCore;
//using StorageAppMvc.Data;
using System.ComponentModel.DataAnnotations;


namespace Domain
{
    public class Item : IEntity
    {
        //Item properties
        [Required]
        public int Id { get; set; }
        [Required]
        public string? Name { get; set; }
        public string? Description { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.Now;
        //public Image image { get; set; } ???
        public int Quantity { get; set; } = 1;

        public int? ContainerId { get; set; }
        public Container? Container { get; set; }


        //private readonly StorageDb? _context;

        //public Item(StorageDb context)
        //{
        //    _context = context;
        //}



        // Constructors//
        // This constructor is for when you first create an item to be added to the database.
        public Item(string name, string? description)
        {
            Name = name;
            Description = description;
        }

        // This overloaded constructor is for when you first create an item to be added to the database. AND you want to add a quantity
        public Item(string name, string? description, int quantity)
        {
            Name = name;
            Description = description;
            Quantity = quantity;
        }

        // This overloaded constructor is for when you load existing items from the database.
        public Item(int id, string name, string? description, int quantity, DateTime dateTime)
        {
            Id = id;
            Name = name;
            Description = description;
            Quantity = quantity;
            AddedOn = dateTime;
        }

        public Item()
        {

        }



        // Item specific methods //
        public void Move(Container? currentContainer, Container targetContainer)
        {
            // if the item is in a container add it to the targetcontainer
            // if the item isnt in a container add it to the targetcontainer


        }

        // IEntity methods to CRUD an item
        public void Create()
        {
            //_context.Add(this);
            //_context.SaveChanges();
        }

        public void Delete()
        {
            //// This presents some difficulties, because the item in C# isnt the exact same item in the database //
            //// Therefore we will look for the item in the database that matches the unique Id of the item we present it //
            //// The item we find in the database can then be used to remove it //

            //var itemToDelete = _context.Items.FirstOrDefault(item => item.Id == Id);

            //// We have to check for null incase it doesn't find anything.
            //if (itemToDelete != null)
            //{
            //    // Remove the item
            //    _context.Remove(itemToDelete);
            //    _context.SaveChanges();
            //}
        }

        public void Update()
        {
            //var itemToUpdate = _context.Items.FirstOrDefault(item => item.Id == Id);

            //if (itemToUpdate != null)
            //{
            //    // Change it's (changable) values, example: If description doesn't change but name does then description will be updated to the same value but name will change.
            //    itemToUpdate.Name = Name;
            //    itemToUpdate.Description = Description;
            //    itemToUpdate.Quantity = Quantity;

            //    // Update the values
            //    _context.Update(itemToUpdate);
            //    _context.SaveChanges();
            //}
        }
    }
}
