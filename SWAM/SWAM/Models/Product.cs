using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM
{
    public class Product
    {
        int _id;
        string _name;
        long _weigth;
        long _length;
        long _width;
        long _height;
        decimal _sellingPrice;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public long Weigth { get => _weigth; set => _weigth = value; }
        public long Length { get => _length; set => _length = value; }
        public long Width { get => _width; set => _width = value; }
        public long Height { get => _height; set => _height = value; }
        public decimal SellingPrice { get => _sellingPrice; set => _sellingPrice = value; }
    }
}
