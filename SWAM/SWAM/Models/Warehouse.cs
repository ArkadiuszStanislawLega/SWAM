﻿using System;
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
        IList<Contact> _contacts;
        IList<CustomerOrder> _customerOrders;
        Address _address;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public IList<Contact> Contact { get => _contacts; set => _contacts = value; }
        public Address Address { get => _address; set => _address = value; }
        public IList<CustomerOrder> CustomerOrders { get => _customerOrders; set => _customerOrders = value; }
    }
}
