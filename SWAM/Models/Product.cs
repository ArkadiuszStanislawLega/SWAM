﻿using SWAM.Models.Customer;
using SWAM.Models.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SWAM.Models
{
    /// <summary>
    /// The basic model of the class in the database representing products in <see cref="Warehouse"/>, <see cref="WarehouseOrder"/> and <see cref="CustomerOrder"/>.
    /// </summary>
    public class Product : IComparable
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

        private static ApplicationDbContext _context = new ApplicationDbContext();

        private static ApplicationDbContext context
        {
            get
            {
                return _context;
            }
        }

        #region Get
        /// <summary>
        /// Extracts product from the database by id number
        /// </summary>
        /// <param name="id">Product id number from the database.</param>
        /// <returns>Product with given Id number from the database.</returns>
        public static Product Get(int id)
        {
            _context = new ApplicationDbContext();
            return _context.Products.FirstOrDefault(p => p.Id == id);
        }
        #endregion
        #region Get
        /// <summary>
        /// Retrieves the current product from the database.
        /// </summary>
        /// <returns>Product from the database with current ID number.</returns>
        public Product Get()
        {
            _context = new ApplicationDbContext();
            return _context.Products.FirstOrDefault(p => p.Id == this.Id);
        }
        #endregion
        #region AddNewProduct
        /// <summary>
        /// Adds the specified product to the database.
        /// </summary>
        /// <param name="product">New product to be added to the database.</param>
        public static void AddNewProduct(Product product)
        {
            if (product != null)
            {
                context.Products.Add(product);
                context.SaveChanges();
            }
        }
        #endregion
        #region EditProduct
        /// <summary>
        /// Edits product values in the database.
        /// </summary>
        /// <param name="product">Product with edited values.</param>
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
        #endregion

        //TODO: Consider the option of removing the product from the database.
        public static void DeleteProduct(Product product)
        {
            context.Products.Remove(context.Products.FirstOrDefault(p => p.Id == product.Id));
            context.SaveChanges();
        }

        #region AllProducts
        /// <summary>
        /// Gets a list of all products from the database.
        /// </summary>
        /// <returns>Returns a list with all products in the database.</returns>
        public static List<Product> AllProducts()
		{
			_context = new ApplicationDbContext();
			return context.Products.ToList();
		}
        #endregion

        #region CompareTo
        /// <summary>
        /// Comparing two products with each other.
        /// </summary>
        /// <param name="obj">Product with which the current product is to be compared.</param>
        /// <returns>1 - prodducts are same, 0 products are different, -1 - error</returns>
        public int CompareTo(object obj)
        {
            if (obj != null && obj is Product compareProduct)
            {
                if (this.Id == compareProduct.Id &&
                    this.Height == compareProduct.Height &&
                    this.Name == compareProduct.Name &&
                    this.Length == compareProduct.Length &&
                    this.Width == compareProduct.Width &&
                    this.Weigth == compareProduct.Weigth &&
                    this.Price == compareProduct.Price &&
                    Get(Id) == this) { return 1; }
                else
                { return 0; }
            }
            else
                return -1;
        }
        #endregion
    }
}
