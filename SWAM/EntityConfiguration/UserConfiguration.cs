using SWAM.Models.User;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Infrastructure.Annotations;
using System.Data.Entity.ModelConfiguration;

namespace SWAM.EntityConfiguration
{
    public class UserConfiguration : EntityTypeConfiguration<User>
    {
        public UserConfiguration()
        {
            ToTable("Users");

            HasMany(u => u.Phones)
                .WithRequired(u => u.User);

            HasMany(u => u.EmailAddresses)
                .WithRequired(u => u.User);

            HasMany(u => u.Messages)
                .WithRequired(m => m.Receiver);

            HasMany(u => u.Messages)
               .WithRequired(m => m.Sender);

            HasMany(u => u.Accesess);
        }
    }
}
