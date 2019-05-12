using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM
{
    public class ExternalSupplayer
    {
        int _id;
        int _conterpartyId;
        string _name;
        Address _address;
        IList<Contact> _contacts;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public Address Address { get => _address; set => _address = value; }
        public IList<Contact> Contacts { get => _contacts; set => _contacts = value; }
        public int ConterpartyId { get => _conterpartyId; set => _conterpartyId = value; }
    }
}
