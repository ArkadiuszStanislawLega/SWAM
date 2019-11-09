using SWAM.Models.ExternalSupplier;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.EntityConfiguration
{
    public class ExternalSupplierPhoneConfiguration : EntityTypeConfiguration<ExternalSupplierPhone>
    {

        public ExternalSupplierPhoneConfiguration()
        {
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
