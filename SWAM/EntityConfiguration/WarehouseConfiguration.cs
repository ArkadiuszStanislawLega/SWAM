using SWAM.Models;
using System.Data.Entity.ModelConfiguration;


namespace SWAM.EntityConfiguration
{
    public class WarehouseConfiguration : EntityTypeConfiguration<Warehouse>
    {
        public WarehouseConfiguration()
        {
            HasRequired(w => w.WarehouseAddress)
                .WithRequiredPrincipal(a => a.Warehouse);
        }
    }
}
