using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
    public class Phone
    {
        int _id;
        string _note;
        string _phoneNumber;

        [Required]
        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int Id { get => _id; set => _id = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string Note { get => _note; set => _note = value; }

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
        public static IEnumerable<Phone> GetUserPhones(int userId) => _context.Phones.ToList().Where(u => u.UserId == userId);
        #endregion
        #region GetPhoneById
        /// <summary>
        /// Geting phone number from database with specific id phone number from database.
        /// </summary>
        /// <param name="phoneId">Phone id in database.</param>
        /// <returns>Phone from database.</returns>
        public static Phone GetPhoneById(int phoneId) => _context.Phones.FirstOrDefault(p => p.Id == phoneId);
        #endregion

        #region AddNewPhone
        /// <summary>
        /// Adding new Phone to database.
        /// </summary>
        /// <param name="phone"></param>
        public static void AddNewPhone(Phone phone)
        {
            if (phone != null)
            {
                _context.Phones.Add(phone);
                _context.SaveChanges();
            }
        }
        #endregion

        #region UpdateNumber
        /// <summary>
        /// Update current phone number.
        /// </summary>
        /// <param name="newPhoneNumber">New phone/edited number.</param>
        public void UpdateNumber(string newPhoneNumber)
        {
            _context.Phones.FirstOrDefault(p => p.Id == this.Id).PhoneNumber = newPhoneNumber;
            _context.SaveChanges();
        }
        #endregion
        #region UpdateNote
        /// <summary>
        /// Update in database note of current phone number.
        /// </summary>
        /// <param name="newNote">New/edited note of phone number.</param>
        public void UpdateNote(string newNote)
        {
            _context.Phones.SingleOrDefault(p => p.Id == this._id).Note = newNote;
            _context.SaveChanges();
        }
        #endregion
        #region Delete
        /// <summary>
        /// Delete from database current number.
        /// </summary>
        public void Delete()
        {
            var phone = _context.Phones.FirstOrDefault(p => p.Id == this.Id);
            if (phone != null)
            {
                _context.Phones.Remove(phone);
                _context.SaveChanges();
            }
        }
        #endregion

        public override string ToString() => $"{this._note} - {this._phoneNumber}";
    }
}
