using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWAM.Enumerators;

namespace SWAM.Models
{
    public class CustomerOrder
    {
        int _id;
        /// <summary>
        /// Indicates if order has been paid
        /// </summary>
        bool _isPaid;
        /// <summary>
        /// Date when order was made
        /// </summary>
        DateTime _orderDate;
        /// <summary>
        /// Date when products were delivered to customer
        /// </summary>
        DateTime? _deliveryDate;
        Warehouse _warehouse;
        Customer _customer;
        Courier _courier;
        IList<CustomerOrderPosition> _customerOrderPositions;
        CustomerOrderStatus _customerOrderStatus;
        ShipmentType _ShipmentType;
        PaymentType _paymentType;

        public int Id { get => _id; set => _id = value; }
        public bool IsPaid { get => _isPaid; set => _isPaid = value; }
        public DateTime OrderDate { get => _orderDate; set => _orderDate = value; }
        public DateTime? DeliveryDate { get => _deliveryDate; set => _deliveryDate = value; }
        public Warehouse Warehouse { get => _warehouse; set => _warehouse = value; }
        public Customer Customer { get => _customer; set => _customer = value; }
        public Courier Courier { get => _courier; set => _courier = value; }
        public CustomerOrderStatus CustomerOrderStatus { get => _customerOrderStatus; set => _customerOrderStatus = value; }
        public IList<CustomerOrderPosition> CustomerOrderPositions { get => _customerOrderPositions; set => _customerOrderPositions = value; }
        public ShipmentType ShipmentType { get => _ShipmentType; set => _ShipmentType = value; }
        public PaymentType PaymentType { get => _paymentType; set => _paymentType = value; }
    }
}
