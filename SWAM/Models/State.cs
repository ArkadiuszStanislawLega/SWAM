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
    /// The basic class model representing the current status of products in the warehouse.
    /// </summary>
    public class State
    {
        int _id;
        /// <summary>
        /// Product quantity avaiable to sale
        /// </summary>
        int _available;
       
        
        Warehouse.Warehouse _warehouse;
        IList<CustomerOrderPosition> _customerOrderPositions;
        IList<WarehouseOrderPosition> _warehouseOrderPositions;

        public int Id { get => _id; set => _id = value; }
        public int Available { get => _available; set => _available = value; }
        /// <summary>
        /// Product quantity booked for buyer
        /// </summary>
        public int Booked { get; set; }
        public int Quantity { get; set; }
        public IList<CustomerOrderPosition> CustomerOrderPositions { get => _customerOrderPositions; set => _customerOrderPositions = value; }
        public IList<WarehouseOrderPosition> WarehouseOrderPositions { get => _warehouseOrderPositions; set => _warehouseOrderPositions = value; }
        public Warehouse.Warehouse Warehouse { get => _warehouse; set => _warehouse = value; }
    }
}
