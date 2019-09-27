using SWAM.Models;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class CustomerConfiguration : EntityTypeConfiguration<Customer>
    {
        public CustomerConfiguration()
        {
            ToTable("Customers");

            HasMany(user => user.Phones)
                    .WithRequired(phone => (Customer)phone.Person)
                    .HasForeignKey(phone => phone.Person);
        }
    }
}
