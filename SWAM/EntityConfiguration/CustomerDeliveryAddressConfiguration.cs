using SWAM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.EntityConfiguration
{
    public class CustomerDeliveryAddressConfiguration : EntityTypeConfiguration<CustomerDeliveryAddress>
    {
        public CustomerDeliveryAddressConfiguration()
        {
            HasRequired(a => a.Customer)
                .WithRequiredDependent(c => c.CustomerDeliveryAddress);
        }
    }
}
