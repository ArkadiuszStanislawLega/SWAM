using SWAM.Models;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class CourierConfiguration : EntityTypeConfiguration<Courier>
    {
        public CourierConfiguration()
        {
            HasOptional(courier => courier.Phone);
        }
    }
}
