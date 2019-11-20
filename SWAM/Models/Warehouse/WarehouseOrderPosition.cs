
namespace SWAM.Models.Warehouse
{
    /// <summary>
    /// The basic class model in the database representing a single item from the product order to the warehouse.
    /// </summary>
    public class WarehouseOrderPosition
    {
        /// <summary>
        /// Identification number from the single item database from the product order for the warehouse.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Product quantity.
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// Product from the order.
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// Product's foreign key property
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Warehouse with orders.
        /// </summary>
        public WarehouseOrder WarehouseOrder { get; set; }
    }
}
