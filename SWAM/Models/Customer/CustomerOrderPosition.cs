namespace SWAM.Models.Customer
{
    /// <summary>
    /// The basic class model in the database representing the <see cref="CustomerOrder"/> item.
    /// </summary>
    public class CustomerOrderPosition
    {
        /// <summary>
        /// Customer order postion number Id in database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Product Id from database.
        /// </summary>
        public int ProductId { get; set; }
        /// <summary>
        /// Product this item applies to.
        /// </summary>
        public Product Product { get; set; }
        /// <summary>
        /// The quantity of product contained in the item.
        /// </summary>
        public int Quantity { get; set; }
        /// <summary>
        /// The price for the product applies to this item.
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Order number to which this item applies.
        /// </summary>
        public CustomerOrder CustomerOrder { get; set; }
        /// <summary>
        /// State that product belong to
        /// </summary>
        public State State { get; set; }
    }
}
