using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWAM.Enumerators;

namespace SWAM.Models
{
    public class WarehouseOrder
    {
        int _id;
        WarehouseOrderStatus _warehouseOrderStatus;
        DateTime _orderDate;
        DateTime _deliveryDate;
        Warehouse _warehouse;
        ExternalSupplier _externalSupplayer;

        public int Id { get => _id; set => _id = value; }
        public DateTime OrderDate { get => _orderDate; set => _orderDate = value; }
        public DateTime DeliveryDate { get => _deliveryDate; set => _deliveryDate = value; }
        public Warehouse Warehouse { get => _warehouse; set => _warehouse = value; }
        public ExternalSupplier ExternalSupplayer { get => _externalSupplayer; set => _externalSupplayer = value; }
        public WarehouseOrderStatus WarehouseOrderStatus { get => _warehouseOrderStatus; set => _warehouseOrderStatus = value; }
    }
}
