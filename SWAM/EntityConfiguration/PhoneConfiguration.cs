using SWAM.Models;
using System.Data.Entity.ModelConfiguration;


namespace SWAM.EntityConfiguration
{
    public class PhoneConfiguration : EntityTypeConfiguration<Phone>
    {
        public PhoneConfiguration()
        {
            ToTable("Phones");
        }
    }
}
