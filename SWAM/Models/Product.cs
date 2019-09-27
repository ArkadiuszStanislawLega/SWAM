using System;
using System.Collections.Generic;
using System.Linq;
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
        ApplicationDbContext _context = new ApplicationDbContext();

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

        public List<Product> GetProducts() { return _context.Products.ToList(); }
    }
}
