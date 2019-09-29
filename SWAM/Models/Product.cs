using SWAM.Models.Customer;
using SWAM.Models.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
    /// <summary>
    /// The basic model of the class in the database representing products in <see cref="Warehouse"/>, <see cref="WarehouseOrder"/> and <see cref="CustomerOrder"/>.
    /// </summary>
    public class Product
    {
        /// <summary>
        /// Identification number from the product database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Product own name.
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Product Weight.
        /// </summary>
        public long Weigth { get; set; }
        /// <summary>
        /// Product Length.
        /// </summary>
        public long Length { get; set; }
        /// <summary>
        /// Product Width.
        /// </summary>
        public long Width { get; set; }
        /// <summary>
        /// Product Height.
        /// </summary>
        public long Height { get; set; }
        /// <summary>
        /// Product Price.
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// Items in warehouse orders where the product is used.
        /// </summary>
        public IList<WarehouseOrderPosition> WarehouseOrderPositions { get; set; }
        /// <summary>
        /// Items in customer orders where the product is used.
        /// </summary>
        public IList<CustomerOrderPosition> CustomerOrderPositions { get; set; }
        public List<Product> GetProducts() { throw new NotImplementedException(); /* return _context.Products.ToList();*/ }
    }
}
