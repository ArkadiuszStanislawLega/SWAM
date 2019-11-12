using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace SWAM.Models
{
    /// <summary>
    /// The basic class model in the database representing the telephone number.
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// Identification number from the phone number database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Phone number.
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// Note regarding the telephone number.
        /// </summary>
        public string Note { get; set; }

        private static readonly ApplicationDbContext DB_CONTEXT = new ApplicationDbContext();

        private static ApplicationDbContext _context
        {
            //TODO: Make all exceptions
            get
            {
                return DB_CONTEXT;
            }

        }

        #region GetUserPhones
        /// <summary>
        /// Make list of specific user phone numbers.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List with phones of user.</returns>
        public static IEnumerable<Phone> GetUserPhones(int userId)
        {
            throw new NotImplementedException();
            //return _context.Phones.ToList().Where(u => u.UserId == userId);
        }
        #endregion
        #region GetPhoneById
        /// <summary>
        /// Geting phone number from database with specific id phone number from database.
        /// </summary>
        /// <param name="phoneId">Phone id in database.</param>
        /// <returns>Phone from database.</returns>
        public static Phone GetPhoneById(int phoneId)
        {
            throw new NotImplementedException();
            //_context.Phones.FirstOrDefault(p => p.Id == phoneId);
        }
        #endregion
        public override string ToString() => $"{this.Note} - {this.PhoneNumber}";
    }
}
