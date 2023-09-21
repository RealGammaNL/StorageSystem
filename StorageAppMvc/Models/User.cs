using StorageSystem.Data;
using System.ComponentModel.DataAnnotations;

namespace StorageSystem.Models
{
    public class User : IEntity
    {
        // User properties //

        [Required]
        public int Id { get; set; }
        public string? Name { get; set; }
        [Required]
        public string? Email { get; set; }
        [Required]
        public string? Password { get; set; }
        public List<Room>? Rooms { get; set; }


        // User specific methods //
        public void Login(string username, string password)
        {

        }
        public void Logout() 
        { 
        
        }

        // IEntity methods to CRUD a user //
        private StorageDb context = new StorageDb();
        public void Create()
        {
            context.Add(this);
            context.SaveChanges();
        }

        public void Delete()
        {
            var userToDelete = context.Users.FirstOrDefault(user => user.Id == Id);

            // We have to check for null incase it doesn't find anything.
            if (userToDelete != null)
            {
                // Remove the user
                context.Remove(userToDelete);
                context.SaveChanges();
            }
        }

        public void Update()
        {
            var userToUpdate = context.Users.FirstOrDefault(user => user.Id == Id);

            if (userToUpdate != null)
            {
                // Change it's (changable) values
                userToUpdate.Name = Name;
                userToUpdate.Email = Email;
                userToUpdate.Password = Password;

                // Update the values
                context.Update(userToUpdate);
                context.SaveChanges();
            }
        }

        // Constructors
        // This constructor is for when you first create a user to be added to the database.
        public User(string name, string email, string password)
        {
            Name = name;
            Email = email;
            Password = password;
        }

        // This constructor is for when you want to edit a user but dont know the amount of rooms.
        public User(int id, string name, string email, string password)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
        }

        // This overloaded constructor is for when you load existing users from the database.
        public User(int id, string name, string email, string password, List<Room> rooms)
        {
            Id = id;
            Name = name;
            Email = email;
            Password = password;
            Rooms = rooms;
        }
    }
}
