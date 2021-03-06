﻿using SWAM.Models;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class AccessesConfiguration : EntityTypeConfiguration<AccessUsersToWarehouses>
    {
        public AccessesConfiguration()
        {
            HasRequired(a => a.Administrator);

            HasRequired(a => a.User)
                .WithMany(u=>u.Accesess);

            HasRequired(a => a.Warehouse);
        }
    }
}
