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
        string _surname;
        List<Email> _emails;
        List<Phone> _phones;
        Address _residentAddress;
        Address _deliveryAddress;
        IList<CustomerOrder> _customerOrders;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public Address ResidentAddress { get => _residentAddress; set => _residentAddress = value; }
        public Address DeliveryAddress { get => _deliveryAddress; set => _deliveryAddress = value; }
        public IList<CustomerOrder> CustomerOrders { get => _customerOrders; set => _customerOrders = value; }
        public string Surname { get => _surname; set => _surname = value; }
        public List<Email> Emails { get => _emails; set => _emails = value; }
        public List<Phone> Phones { get => _phones; set => _phones = value; }
    }
}
