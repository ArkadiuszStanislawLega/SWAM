using SWAM.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;


namespace SWAM.EntityConfiguration
{
    public class PhoneConfiguration : EntityTypeConfiguration<Phone>
    {
        public PhoneConfiguration()
        {
            ToTable("Phones");

            Property(p => p.Id)
               .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
