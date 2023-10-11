using Microsoft.EntityFrameworkCore;
using Domain;


namespace Domain.Data
{
    public class StorageDb : DbContext
    {
        public DbSet<Item> Items { get; set; }
        public DbSet<Container> Containers { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            //string connection = @"Data Source=.;Initial Catalog=StorageDb;Integrated Security=True;TrustServerCertificate=True;";
            //optionsBuilder.UseSqlServer(connection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Specify
            modelBuilder.Entity<Item>()
                .Property(e => e.Name)
                .HasMaxLength(30);
        }

        public StorageDb(DbContextOptions<StorageDb> contextOptions)
            : base(contextOptions)
        {

        }
    }
}
