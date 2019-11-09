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
    public class ExternalSupplierPhoneConfiguartion : EntityTypeConfiguration<ExternalSupplierPhone>
    {
        public ExternalSupplierPhoneConfiguartion()
        {
            Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
        }
    }
}
