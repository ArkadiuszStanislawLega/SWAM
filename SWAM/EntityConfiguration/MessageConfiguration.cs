using SWAM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.EntityConfiguration
{
    public class MessageConfiguration : EntityTypeConfiguration<Message>
    {
        public MessageConfiguration()
        {
            ToTable("Messages");

            Property(p => p.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            //HasRequired(s => s.Sender)
            //    .WithMany(s => s.Messages);

            //HasRequired(s => s.Receiver)
            //    .WithMany(s => s.Messages);
        }
    }
}
