using SWAM.Models.Customer;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customers");

            HasRequired(c => c.ResidentalAddress)
                .WithRequiredPrincipal(c => c.Customer);

            HasOptional(c => c.DeliveryAddress)
                .WithOptionalPrincipal(c => c.Customer);

            HasRequired(c => c.Phone)
                .WithRequiredPrincipal(c => c.Customer);

            HasRequired(c => c.EmailAddress)
                .WithRequiredPrincipal(e => e.Customer);

            HasMany(c => c.Orders);
        }
    }
}
