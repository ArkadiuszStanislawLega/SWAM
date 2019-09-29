
using SWAM.Models.Warehouse;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class WarehouseOrderConfiguration : EntityTypeConfiguration<WarehouseOrder>
    {
        public WarehouseOrderConfiguration()
        {
            HasRequired(w => w.ExternalSupplayer);
            HasRequired(w => w.Warehouse);

            HasMany(w => w.OrderPositions)
                 .WithRequired(w => w.WarehouseOrder);
        }
    }
}
