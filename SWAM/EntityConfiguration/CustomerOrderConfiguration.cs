using SWAM.Models.Customer;
using System.Data.Entity.ModelConfiguration;
namespace SWAM.EntityConfiguration
{
    public class CustomerOrderConfiguration : EntityTypeConfiguration<CustomerOrder>
    {
        public CustomerOrderConfiguration()
        {
            HasRequired(c => c.Customer);
            HasRequired(c => c.Creator);
            HasRequired(c => c.Warehouse)
                .WithMany(w=>w.CustomerOrders);
            HasOptional(c => c.Courier);

            HasOptional(c => c.DeliveryAddress)
                .WithOptionalPrincipal(c => c.CustomerOrder);
        }
    }
}
