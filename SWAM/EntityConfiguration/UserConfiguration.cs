using SWAM.Models;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");

            HasMany(user => user.Phones)
            .WithRequired(phone => (User)phone.Person);

            HasMany(user => user.EmailAddresses)
            .WithRequired(email => (User)email.Person)
            .WillCascadeOnDelete(false);
        }
    }
}
