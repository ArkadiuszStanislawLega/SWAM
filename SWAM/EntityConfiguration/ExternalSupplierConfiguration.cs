using SWAM.Models;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class ExternalSupplierConfiguration : EntityTypeConfiguration<ExternalSupplier>
    {
        public ExternalSupplierConfiguration()
        {
            HasMany(user => user.Phones)
            .WithRequired(phone => (ExternalSupplier)phone.Person);
        }
    }
}
