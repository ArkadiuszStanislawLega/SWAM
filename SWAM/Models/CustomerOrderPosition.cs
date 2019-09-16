using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
    /// <summary>
    /// Stores exact products of customer's order
    /// </summary>
    public class CustomerOrderPosition
    {
        int _id;
        decimal _price;
        uint quantity;
        Product _product;
        CustomerOrder _customerOrder;

        public int Id { get => _id; set => _id = value; }
        public decimal Price { get => _price; set => _price = value; }
        public uint Quantity { get => quantity; set => quantity = value; }
        public Product Product { get => _product; set => _product = value; }
        public CustomerOrder CustomerOrder { get => _customerOrder; set => _customerOrder = value; }
    }
}
