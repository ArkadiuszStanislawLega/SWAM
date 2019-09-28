using System.ComponentModel.DataAnnotations.Schema;

namespace SWAM.Models
{
    [Table("WarehouseAddress")]
    public class WarehouseAddress : Address
    {
        public Warehouse Warehouse { get; set; }
    }
}
