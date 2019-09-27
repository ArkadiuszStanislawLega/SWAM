using SWAM.Models;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class EmailAddressConfiguration : EntityTypeConfiguration<EmailAddress>
    {
        public EmailAddressConfiguration()
        {
            HasRequired(email => email.Person)
            .WithMany()
            .WillCascadeOnDelete(false);
        }
    }
}
