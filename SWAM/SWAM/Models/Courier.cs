using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
    public class Courier
    {
        int _id;
        string _name;
        /// <summary>
        /// Tax Identification Number
        /// </summary>
        string _tin;
        IList<Address> _addresses;
        IList<Contact> _contacts;
        IList<CustomerOrder> _customerOrders;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public IList<Address> Addresses { get => _addresses; set => _addresses = value; }
        public IList<Contact> Contacts { get => _contacts; set => _contacts = value; }
        public string Tin { get => _tin; set => _tin = value; }
        public IList<CustomerOrder> CustomerOrders { get => _customerOrders; set => _customerOrders = value; }
    }
}
