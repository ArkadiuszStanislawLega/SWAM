using SWAM.Models;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class CourierConfiguration : EntityTypeConfiguration<Courier>
    {
        public CourierConfiguration()
        {
            ToTable("Couriers");

            //HasOptional(courier => courier.Phone)
            //    .WithRequired(phone => (Courier)phone.Person)
            //    .WillCascadeOnDelete(false);
        }
    }
}
