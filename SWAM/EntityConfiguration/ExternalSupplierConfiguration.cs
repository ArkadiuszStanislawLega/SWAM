using SWAM.Models;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class ExternalSupplierConfiguration : EntityTypeConfiguration<ExternalSupplier>
    {
        public ExternalSupplierConfiguration()
        {
            ToTable("ExternalSuppliers");

            //HasMany(user => user.Phones)
            //        .WithRequired(phone => (ExternalSupplier)phone.Person)
            //        .HasForeignKey(phone => phone.Person);
        }
    }
}
