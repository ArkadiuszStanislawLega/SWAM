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
        public virtual DbSet<Address> Adresses { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .HasRequired(p => p.Phone)
                .WithRequiredPrincipal(p => p.Person);

            //modelBuilder.Entity<Person>()
            //    .HasRequired(p => p.Address)
            //    .WithRequiredPrincipal(a => a.Person);

            modelBuilder.Entity<WarehouseAddress>()
                .HasRequired(w => w.Warehouse)
                .WithRequiredDependent(w => w.WarehouseAddress);

            modelBuilder.Configurations.Add(new CourierConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new CustomerDeliveryAddressConfiguration());
            modelBuilder.Configurations.Add(new EmailAddressConfiguration());
            modelBuilder.Configurations.Add(new ExternalSupplierConfiguration());
            modelBuilder.Configurations.Add(new PhoneConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
           
            base.OnModelCreating(modelBuilder);
        }
    }
}
