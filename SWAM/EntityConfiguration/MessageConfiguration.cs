using SWAM.Models;
using System;
using System.Collections.Generic;
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
            HasRequired(m => m.Sender);
            HasRequired(m => m.Receiver);
        }
    }
}
