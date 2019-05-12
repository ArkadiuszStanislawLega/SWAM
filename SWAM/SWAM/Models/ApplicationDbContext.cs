﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SWAM
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ExternalSupplayer> ExternalSupplayers { get; set; }
        public DbSet<OrderToWarehouse> OrderToWarehouses { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; }

        public ApplicationDbContext()
            : base("name=DefaultConnection")
        {
        }
    }
}
