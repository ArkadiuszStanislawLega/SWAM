using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM
{
    public class User
    {
        string _name;
        string _password;
        UsersType _permissions;

        public string Name { get => _name; set => _name = value; }
        public string Password { get => _password; set => _password = value; }
        public UsersType Permissions { get => _permissions; set => _permissions = value; }

        public User(string name, string password, UsersType permissions)
        {
            this._name = name;
            this._password = password;
            this._permissions = permissions;
        }

    }
}
