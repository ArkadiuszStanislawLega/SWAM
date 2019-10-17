using SWAM.Models.Customer;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.EntityConfiguration
{
    class CustomerOrderPositionConfiguration : EntityTypeConfiguration<CustomerOrderPosition>
    {
        public CustomerOrderPositionConfiguration()
        {
            HasRequired(c => c.Product)
            .WithMany(p => p.CustomerOrderPositions)
            .HasForeignKey(p => p.ProductId);
        }
    }
}
