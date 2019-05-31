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
        string _note;
        string _phoneNumber;

        public int Id { get => _id; set => _id = value; }
        public User User { get => _user; set => _user = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string Note { get => _note; set => _note = value; }
    }
}
