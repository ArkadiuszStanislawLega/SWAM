using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SWAM.Enumerators;

namespace SWAM
{
    public class User
    {
        string _name;
        string _password;
        UserType _permissions;

        public string Name { get => _name; set => _name = value; }
        public string Password { get => _password; set => _password = value; }
        public UserType Permissions { get => _permissions; set => _permissions = value; }

        public User(string name, string password, UserType permissions)
        {
            this._name = name;
            this._password = password;
            this._permissions = permissions;
        }

    }
}
