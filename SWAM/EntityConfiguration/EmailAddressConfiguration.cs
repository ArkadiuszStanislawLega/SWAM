using SWAM.Models;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class EmailAddressConfiguration : EntityTypeConfiguration<EmailAddress>
    {
        public EmailAddressConfiguration()
        {
            ToTable("EmailAddresses");


        }
    }
}
