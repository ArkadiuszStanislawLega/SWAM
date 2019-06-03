using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWAM.Models
{
    public class AccessUsersToWarehouses
    {
        int _id;
        [Key]
        [ForeignKey("User")]
        int _userId;
        [Key]
        [ForeignKey("Warehouse")]
        int _warehouseId;

        public int Id { get => _id; set => _id = value; }
        public int UserId { get => _userId; set => _userId = value; }
        public int WarehouseId { get => _warehouseId; set => _warehouseId = value; }
    }
}