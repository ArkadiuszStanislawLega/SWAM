using SWAM.Models.Warehouse;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class WarehouseConfiguration : EntityTypeConfiguration<Warehouse>
    {
        public WarehouseConfiguration()
        {
            ToTable("Warehouses");

            HasRequired(w => w.WarehouseAddress)
                .WithRequiredDependent(a => a.Warehouse);

            HasMany(w => w.Accesses);
            HasMany(w => w.CustomerOrders);
            HasMany(w => w.WarehouseOrders);
        }
    }
}
