using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
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
        StatusOfUserAccount _statusOfUserAccount;
        DateTime _dateOfCreate;
        DateTime? _dateOfExpiryOfTheAccount; 
        DateTime? _expiryDateOfTheBlockade;
        IList<Phone> _phones;
        IList<Email> _emails;
        Address _residentAddress;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public IList<Phone> Contacts { get => _phones; set => _phones = value; }
        public Address ResidentAddress { get => _residentAddress; set => _residentAddress = value; }
        public UserType Permissions { get => _permissions; set => _permissions = value; }
        public DateTime DateOfCreate { get => _dateOfCreate; set => _dateOfCreate = value; }
        public DateTime? ExpiryDateOfTheBlockade { get => _expiryDateOfTheBlockade; set => _expiryDateOfTheBlockade = value; }
        public DateTime? DateOfExpiryOfTheAccount { get => _dateOfExpiryOfTheAccount; set => _dateOfExpiryOfTheAccount = value; }
        public StatusOfUserAccount StatusOfUserAccount { get => _statusOfUserAccount; set => _statusOfUserAccount = value; }
        public string Password { get => _password; set => _password = value; }
        public IList<Email> Emails { get => _emails; set => _emails = value; }
    }
}
