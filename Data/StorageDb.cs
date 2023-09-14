using Microsoft.EntityFrameworkCore;
using StorageSystem.Models;


namespace StorageSystem.Data
{
    public class StorageDb : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Container> Containers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string connection = @"Data Source=.;Initial Catalog=StorageDb;Integrated Security=True;TrustServerCertificate=True;";
            optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Specify
            modelBuilder.Entity<Item>()
                .Property(e => e.Name)
                .HasMaxLength(30);
        }
    }
}
