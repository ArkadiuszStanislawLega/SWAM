using System.Collections.Generic;
using System;
using System.Data.Entity;
using System.Linq;
using SWAM.Models.User;

namespace SWAM.Models
{
    /// <summary>
    /// The basic class model in the database representing the email address of all people (<see cref="Person"/>) in the application.
    /// </summary>
    public class EmailAddress
    {
        /// <summary>
        /// Email address number Id in database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Person's email address.
        /// </summary>
        public string AddressEmail { get; set; }
        /// <summary>
        /// Note to email address.
        /// </summary>
        public string Note { get; set; }

        private static ApplicationDbContext dbContext = new ApplicationDbContext();

        protected static ApplicationDbContext _context 
        {
            get
            {
                //TODO: Make all exceptions
                return dbContext;
            }
            set => dbContext = value;
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
    }
}
