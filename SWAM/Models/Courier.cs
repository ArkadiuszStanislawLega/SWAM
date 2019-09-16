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
        IList<Phone> _phones;
        IList<CustomerOrder> _customerOrders;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public IList<Address> Addresses { get => _addresses; set => _addresses = value; }
        public IList<Phone> Phones { get => _phones; set => _phones = value; }
        public string Tin { get => _tin; set => _tin = value; }
        public IList<CustomerOrder> CustomerOrders { get => _customerOrders; set => _customerOrders = value; }
    }
}
