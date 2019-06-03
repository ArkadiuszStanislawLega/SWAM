using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
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
        /// <summary>
        /// All user phone numbers.
        /// </summary>
        IList<Phone> _phones;
        /// <summary>
        /// All user emails.
        /// </summary>
        IList<Email> _emails;
        /// <summary>
        /// All ids of warheouses where user have permition to  access.
        /// </summary>
        IList<AccessUsersToWarehouses> _warehousesId;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public UserType Permissions { get => _permissions; set => _permissions = value; }
        public DateTime DateOfCreate { get => _dateOfCreate; set => _dateOfCreate = value; }
        public DateTime? ExpiryDateOfTheBlockade { get => _expiryDateOfTheBlockade; set => _expiryDateOfTheBlockade = value; }
        public DateTime? DateOfExpiryOfTheAccount { get => _dateOfExpiryOfTheAccount; set => _dateOfExpiryOfTheAccount = value; }
        public StatusOfUserAccount StatusOfUserAccount { get => _statusOfUserAccount; set => _statusOfUserAccount = value; }
        public string Password { get => _password; set => _password = value; }

        /// <summary>
        /// Getting from database all email addresses
        /// </summary>
        public IList<Email> Emails
        {
            get
            {
                //Todo: Catch exceptions - User - Emails
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    this._emails = context.Emails
                        .SqlQuery("Select * from Emails where UserId=@userId", new SqlParameter("@userId", this._id))
                        .ToList<Email>();
                };
                return this._emails;
            }
        }

        /// <summary>
        /// Getting from database all warehouses id where user have permition to access.
        /// </summary>
        public IList<AccessUsersToWarehouses> WarehousesId
        {
            get
            {
                //Todo: Catch exceptions - User - WarehousesId
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    this._warehousesId = context.AccessUsersToWarehouses
                        .SqlQuery("Select * from AccessUsersToWarehouses where UserId=@userId", new SqlParameter("@userId", this._id))
                        .ToList<AccessUsersToWarehouses>();
                }; 
                return this._warehousesId;
            }
        }

        /// <summary>
        /// Getting from database all user phone numbers.
        /// </summary>
        public IList<Phone> Phones
        {
            get
            {
                //Todo: Catch exceptions - User - Phones
                using (ApplicationDbContext context = new ApplicationDbContext())
                {
                    this._phones = context.Phones
                        .SqlQuery("Select * from Phones where UserId=@userId", new SqlParameter("@userId", this._id))
                        .ToList<Phone>();
                };
                return this._phones;
            }
        }
    }
}
