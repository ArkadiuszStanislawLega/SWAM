using SWAM.Models.ExternalSupplier;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class ExternalSupplierConfiguration : EntityTypeConfiguration<ExternalSupplier>
    {
        public ExternalSupplierConfiguration()
        {
            ToTable("ExternalSuppliers");
            HasRequired(e => e.Address)
                .WithRequiredPrincipal(e => e.ExternalSupplier);

            HasMany(e => e.WarehouseOrders);

            HasMany(e => e.Phones);

            HasRequired(e => e.EmailAddress)
                .WithRequiredPrincipal(e => e.ExternalSupplier)
                .WillCascadeOnDelete(true);
        }
    }
}
