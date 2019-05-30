using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
    public class Phone
    {
        int _id;
        User _user;
        char _phoneNumber;

        public int Id { get => _id; set => _id = value; }
        public User User { get => _user; set => _user = value; }
        public char PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
    }
}
