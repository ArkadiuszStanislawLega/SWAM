﻿using System.Data.Entity;
using System.Data.Entity.Migrations;
using SWAM.EntityConfiguration;
using SWAM.Models;
using SWAM.Models.Customer;
using SWAM.Models.User;
using SWAM.Models.Warehouse;

namespace SWAM
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<AccessUsersToWarehouses> AccessUsersToWarehouses { get; set; }
        public DbSet<Address> Adresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CustomerPhone> CustomersPhones { get; set; }
        public DbSet<CustomerEmailAddress> CustomerEmailAddresses { get; set; }
        public DbSet<EmailAddress> EmailAddresses { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Phone> Phones { get; set; }
        public DbSet<Person> People { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<State> States { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }
        public DbSet<WarehouseAddress> WarehouseAddresses { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection")
        {

        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new AccessesConfiguration());
            modelBuilder.Configurations.Add(new AddressConfiguration());
            modelBuilder.Configurations.Add(new CourierConfiguration());
            modelBuilder.Configurations.Add(new CustomerConfiguration());
            modelBuilder.Configurations.Add(new CustomerOrderConfiguration());
            modelBuilder.Configurations.Add(new EmailAddressConfiguration());
            modelBuilder.Configurations.Add(new ExternalSupplierConfiguration());
            modelBuilder.Configurations.Add(new MessageConfiguration());
            modelBuilder.Configurations.Add(new PhoneConfiguration());
            modelBuilder.Configurations.Add(new PersonConfiguration());
            modelBuilder.Configurations.Add(new UserConfiguration());
            modelBuilder.Configurations.Add(new WarehouseConfiguration());
            modelBuilder.Configurations.Add(new WarehouseOrderConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}
