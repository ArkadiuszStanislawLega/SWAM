using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
    public class Product
    {
        int _id;
        string _name;
        long _weigth;
        long _length;
        long _width;
        long _height;
        decimal _price;
        IList<State> _states;
        IList<WarehouseOrderPosition> _warehouseOrderPositions;
        IList<CustomerOrderPosition> _customerOrderPositions;
		private static readonly ApplicationDbContext DB_CONTEXT = new ApplicationDbContext();
				
		public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public long Weigth { get => _weigth; set => _weigth = value; }
        public long Length { get => _length; set => _length = value; }
        public long Width { get => _width; set => _width = value; }
        public long Height { get => _height; set => _height = value; }
        public decimal Price { get => _price; set => _price = value; }
        public IList<State> States { get => _states; set => _states = value; }
        public IList<WarehouseOrderPosition> WarehouseOrderPositions { get => _warehouseOrderPositions; set => _warehouseOrderPositions = value; }
        public IList<CustomerOrderPosition> CustomerOrderPositions { get => _customerOrderPositions; set => _customerOrderPositions = value; }
		private static ApplicationDbContext _context
		{
			//TODO: Make all exceptions
			get
			{
				return DB_CONTEXT;
			}
		}
		public Product() { }	

		public Product(string name, long weight, long length, long width, long height, decimal price)
		{

			Name = name;
			Weigth = weight;
			Length = length;
			Width = width;
			Height = height;
			Price = price;
		}
		public static void AddNewProduct(Product product)
		{
			if (product != null)
			{
				_context.Products.Add(product);
				_context.SaveChanges();
			}
		}
		public static void EditProduct(Product product)
		{
			_context.Products.FirstOrDefault(p => p.Id == product.Id).Name = product.Name;
			_context.Products.FirstOrDefault(p => p.Id == product.Id).Weigth = product.Weigth;
			_context.Products.FirstOrDefault(p => p.Id == product.Id).Length = product.Length;
			_context.Products.FirstOrDefault(p => p.Id == product.Id).Width = product.Width;
			_context.Products.FirstOrDefault(p => p.Id == product.Id).Height = product.Height;
			_context.Products.FirstOrDefault(p => p.Id == product.Id).Price = product.Price;
			_context.SaveChanges();
		}

		public static void DeleteProduct(Product product)
		{
			var dbproduct =_context.Products.FirstOrDefault(p => p.Id == product.Id);
			_context.Products.Remove(dbproduct);
			_context.SaveChanges();
		}

		public List<Product> GetProducts()
        {
            return _context.Products.ToList();
        }

        public List<Product> GetProductsFromWarehouse(int warehouseId)
        {
            return _context.States.Include(s => s.Product).Include(s =>s.Warehouse).Where(p => p.WarehouseId == warehouseId).Select(s =>s.Product).ToList();
        }
    }
}
