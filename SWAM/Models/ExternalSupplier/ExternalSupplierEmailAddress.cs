using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models.ExternalSupplier
{
    [Table("ExternalSupplierEmailAddresses")]
    public class ExternalSupplierEmailAddress : EmailAddress
    {
        public ExternalSupplier ExternalSupplier { get; set; }
        public int ExternalSupplierId { get; set; }
    }
}
