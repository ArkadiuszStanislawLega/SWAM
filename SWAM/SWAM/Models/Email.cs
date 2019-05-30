using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
    public class Email
    {
        int _id;
        User _user;
        string _addressEmail;

        public int Id { get => _id; set => _id = value; }
        public User User { get => _user; set => _user = value; }
        public string AddressEmail { get => _addressEmail; set => _addressEmail = value; }
    }
}
