﻿using SWAM.Models.Customer;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customers");

            HasRequired(c => c.ResidentalAddress);
            HasOptional(c => c.DeliveryAddress);

            HasMany(c => c.Orders);
        }
    }
}
