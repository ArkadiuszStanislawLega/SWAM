using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using SWAM.Models;
using SWAM.Enumerators;

namespace SWAM
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<AccessUsersToWarehouses> AccessUsersToWarehouses { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerOrder> CustomerOrders { get; set; }
        public DbSet<CustomerOrderPosition> CustomerOrderPositions { get; set; }
        public DbSet<Email> Emails { get; set; }
        public DbSet<ExternalSupplier> ExternalSupplayers { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseOrder> WarehouseOrders { get; set; }
        public DbSet<WarehouseOrderPosition> WarehouseOrderPositions { get; set; }

        public ApplicationDbContext()
            : base("name=DefaultConnection")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Phone>()
                .HasRequired(c => c.User)
                .WithMany(c => c.Phones)
                .HasForeignKey(u => u.UserId);

            modelBuilder.Entity<Email>()
                .HasRequired(c => c.User)
                .WithMany(c => c.Emails)
                .HasForeignKey(u => u.UserId);               
        }
    }
}
