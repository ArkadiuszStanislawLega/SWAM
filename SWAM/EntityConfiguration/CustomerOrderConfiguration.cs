using SWAM.Models.Customer;
using System.Data.Entity.ModelConfiguration;
namespace SWAM.EntityConfiguration
{
    public class CustomerOrderConfiguration : EntityTypeConfiguration<CustomerOrder>
    {
        public CustomerOrderConfiguration()
        {
            HasRequired(c => c.Customer);
            HasRequired(c => c.User);
            HasRequired(c => c.Warehouse);
            HasOptional(c => c.Courier);
        }
    }
}
