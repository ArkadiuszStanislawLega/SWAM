﻿using System.Collections.Generic;
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

        private static readonly ApplicationDbContext DB_CONTEXT = new ApplicationDbContext();

        protected static ApplicationDbContext _context 
        {
            get
            {
                //TODO: Make all exceptions
                return DB_CONTEXT;
            }
        }

        #region GetUserEmails
        /// <summary>
        /// Make list with email addresses of specific user.
        /// </summary>
        /// <param name="id">User id from database.</param>
        /// <returns>List with email addresses.</returns>
        public static IEnumerable<EmailAddress> GetUserEmails(int id)
            =>  _context.People.OfType<User.User>().FirstOrDefault(u => u.Id == id).EmailAddresses;

        #endregion

        #region AddEmail
        /// <summary>
        /// Add new email to database.
        /// </summary>
        /// <param name="email">New addres email.</param>
        public static void AddUserAddressEmail(User.User user, UserEmailAddress email)
        {
            if (email != null && user != null)
            {
                //TODO: try - catch block is needed ... when excetion will be catch than send false.
                _context.People.OfType<User.User>().Include(u => u.EmailAddresses).FirstOrDefault(u => u.Id == user.Id).EmailAddresses.Add(email);
                _context.SaveChanges();
            }
        }
        #endregion

    }
}
