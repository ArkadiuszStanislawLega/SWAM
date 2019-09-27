using System.Data.Entity;
using SWAM.EntityConfiguration;
using SWAM.Models;

namespace SWAM
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Warehouse> Warehouses { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new CourierConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new EmailAddressConfiguration());
            modelBuilder.Configurations.Add(new ExternalSupplierConfiguration());
            modelBuilder.Configurations.Add(new PhoneConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
