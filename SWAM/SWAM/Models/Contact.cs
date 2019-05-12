using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM
{
    public class Contact
    {
        int _id;
        uint _phoneNumber;
        string _email;

        public int Id { get => _id; set => _id = value; }
        public uint PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string Email { get => _email; set => _email = value; }
    }
}
