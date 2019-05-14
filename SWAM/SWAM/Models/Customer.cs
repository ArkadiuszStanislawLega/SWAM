using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
    public class Customer
    {
        int _id;
        string _name;
        IList<Contact> _contacts;
        Address _residentAddress;
        Address _deliveryAddress;
        IList<CustomerOrder> _customerOrders;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public IList<Contact> Contacts { get => _contacts; set => _contacts = value; }
        public Address ResidentAddress { get => _residentAddress; set => _residentAddress = value; }
        public Address DeliveryAddress { get => _deliveryAddress; set => _deliveryAddress = value; }
        public IList<CustomerOrder> CustomerOrders { get => _customerOrders; set => _customerOrders = value; }
    }
}
