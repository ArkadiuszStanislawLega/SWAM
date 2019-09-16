using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM
{
    public class Address
    {
        int _id;
        string _country;
        string _city;
        string _street;
        string _houseNumber;
        string _apartmentNumber;
        string _postCode;

        public int Id { get => _id; set => _id = value; }
        public string Country { get => _country; set => _country = value; }
        public string City { get => _city; set => _city = value; }
        public string Street { get => _street; set => _street = value; }
        public string HouseNumber { get => _houseNumber; set => _houseNumber = value; }
        public string ApartmentNumber { get => _apartmentNumber; set => _apartmentNumber = value; }
        public string PostCode { get => _postCode; set => _postCode = value; }
    }
}
