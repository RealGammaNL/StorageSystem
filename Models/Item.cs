using StorageSystem.Data;
using System.ComponentModel.DataAnnotations;

namespace StorageSystem.Models
{
    public class Item : IEntity
    {
        //Item properties
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } 
        public string? Description { get; set; }
        public DateTime AddedOn { get; set; } = DateTime.Now;
        //public Image image { get; set; } ???
        public int Quantity { get; set; } = 1;
        public List<Item>? Items { get; internal set; }


        // Item specific methods //
        public void Move(Container? currentContainer, Container targetContainer)
        {
            // if the item is in a container add it to the targetcontainer
            // if the item isnt in a container add it to the targetcontainer

            
        }

        // IEntity methods to CRUD an item
        private StorageDb context = new StorageDb();

        public void Create()
        {
            // Create the item
            context.Add(this);
            context.SaveChanges();
        }

        public void Delete()
        {
            // This presents some difficulties, because the item in C# isnt the exact same item in the database //
            // Therefore we will look for the item in the database that matches the unique Id of the item we present it //
            // The item we find in the database can then be used to remove it //

            var itemToDelete = context.Items.FirstOrDefault(item => item.Id == Id);

            // We have to check for null incase it doesn't find anything.
            if (itemToDelete != null)
            {
                // Remove the item
                context.Remove(itemToDelete);
                context.SaveChanges();
            }
        }

        public void Update()
        {
            var itemToUpdate = context.Items.FirstOrDefault(item => item.Id == Id);

            if (itemToUpdate != null)
            {
                // Change it's (changable) values, example: If description doesn't change but name does then description will be updated to the same value but name will change.
                itemToUpdate.Name = Name;
                itemToUpdate.Description = Description;
                itemToUpdate.Quantity = Quantity;
                //itemToUpdate.ContainerId = 2; // hoe moet ik dit aanvliegen??????????????????????????????????????????????

                // Update the values
                context.Update(itemToUpdate);
                context.SaveChanges();
            }
        }

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
    }
}
