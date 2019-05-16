using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWAM.Enumerators;

namespace SWAM.Models
{
    public class User
    {
        int _id;
        string _name;
        string _password;
        UserType _permissions;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Password { get => _password; set => _password = value; }
        public UserType Permissions { get => _permissions; set => _permissions = value; }
    }
}
