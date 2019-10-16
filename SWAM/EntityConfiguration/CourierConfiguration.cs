using SWAM.Models.Courier;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class CourierConfiguration : EntityTypeConfiguration<Courier>
    {
        public CourierConfiguration()
        {
            ToTable("Couriers");

            HasMany(c => c.CustomerOrders)
            .WithOptional(c => c.Courier)
            .HasForeignKey(c => c.CourierId);
        }
    }
}
