using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
   public class State
    {
        int _id;
        /// <summary>
        /// Product quantity avaiable to sale
        /// </summary>
        int _available;
        /// <summary>
        /// Product quantity booked for buyer
        /// </summary>
        int _booked;
        int _quantity;
        int? _warehouseId;
        Warehouse _warehouse;
        Product _product;
        IList<CustomerOrderPosition> _customerOrderPositions;
        IList<WarehouseOrderPosition> _warehouseOrderPositions;

        public int Id { get => _id; set => _id = value; }
        public int Available { get => _available; set => _available = value; }
        public int Booked { get => _booked; set => _booked = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
        public IList<CustomerOrderPosition> CustomerOrderPositions { get => _customerOrderPositions; set => _customerOrderPositions = value; }
        public IList<WarehouseOrderPosition> WarehouseOrderPositions { get => _warehouseOrderPositions; set => _warehouseOrderPositions = value; }
        public Warehouse Warehouse { get => _warehouse; set => _warehouse = value; }
        public int? WarehouseId { get => _warehouseId; set => _warehouseId = value; }
        public Product Product { get => _product; set => _product = value; }
    }
}
