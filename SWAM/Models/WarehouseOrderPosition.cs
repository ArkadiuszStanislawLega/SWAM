using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
    public class WarehouseOrderPosition
    {
        int _id;
        decimal _price;
        uint _quantity;
        Product _product;
        WarehouseOrder _warehouseOrder;
        State _state;

        public int Id { get => _id; set => _id = value; }
        public decimal Price { get => _price; set => _price = value; }
        public uint Quantity { get => _quantity; set => _quantity = value; }
        public Product Product { get => _product; set => _product = value; }
        public WarehouseOrder WarehouseOrder { get => _warehouseOrder; set => _warehouseOrder = value; }
        public State State { get => _state; set => _state = value; }
    }
}
