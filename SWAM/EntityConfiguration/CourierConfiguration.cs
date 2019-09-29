using SWAM.Models.Courier;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class CourierConfiguration : EntityTypeConfiguration<Courier>
    {
        public CourierConfiguration()
        {
            ToTable("Couriers");

            HasRequired(c => c.Phone)
                .WithRequiredDependent(p => p.Courier);

            HasMany(c => c.CustomerOrders);
        }
    }
}
