
namespace SWAM.Models.Warehouse
{
    /// <summary>
    /// The basic model of the class in the database representing the <see cref="Address"/> of the <see cref="SWAM.Models.Warehouse"/>.
    /// </summary>
    public class WarehouseAddress : Address
    {
        /// <summary>
        /// Warehouse to which the address belongs.
        /// </summary>
        public Warehouse Warehouse { get; set; }
    }
}
