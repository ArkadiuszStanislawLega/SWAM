using SWAM.Models.Customer;
using SWAM.Models.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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
        /// Products in state
        /// </summary>
        public IList<State> States { get; set; }
        /// <summary>
        /// Items in warehouse orders where the product is used.
        /// </summary>
        public IList<WarehouseOrderPosition> WarehouseOrderPositions { get; set; }
        /// <summary>
        /// Items in customer orders where the product is used.
        /// </summary>
        public IList<CustomerOrderPosition> CustomerOrderPositions { get; set; }
        public List<Product> GetProducts() { throw new NotImplementedException(); /* return _context.Products.ToList();*/ }

        private static readonly ApplicationDbContext _context = new ApplicationDbContext();

        private static ApplicationDbContext context
        {
            get
            {
                return _context;
            }
        }

        public static void AddNewProduct(Product product)
        {
            if (product != null)
            {
                context.Products.Add(product);
				context.SaveChanges();
			}
        }

        public static void EditProduct(Product product)
        {
            if (product != null)
            {
                var dbProduct = context.Products.FirstOrDefault(p => p.Id == product.Id);

                dbProduct.Name = product.Name;
                dbProduct.Weigth = product.Weigth;
                dbProduct.Length = product.Length;
                dbProduct.Width = product.Width;
                dbProduct.Height = product.Height;
                dbProduct.Price = product.Price;
                context.SaveChanges();
            }
        }

        public static void DeleteProduct(Product product)
		{   
				context.Products.Remove(context.Products.FirstOrDefault(p => p.Id == product.Id));
				context.SaveChanges();
        }

        public static List<Product> AllProducts() => context.Products.ToList();

        public static List<Product> GetProductsFromWarehouse(int warehouseId) 
            => context.States
                    .Include(s => s.Product)
                    .Include(s => s.Warehouse)
                    .Where(p => p.WarehouseId == warehouseId)
                    .Select(s => s.Product).ToList();
    }
}
