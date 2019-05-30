using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
    public class Warehouse
    {
        int _id;
        string _name;
        IList<User> _users;
        IList<CustomerOrder> _customerOrders;
        Address _address;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public IList<User> Users { get => _users; set => _users = value; }
        public Address Address { get => _address; set => _address = value; }
        public IList<CustomerOrder> CustomerOrders { get => _customerOrders; set => _customerOrders = value; }
    }
}
