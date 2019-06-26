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

        private static ApplicationDbContext context()
        {
            //TODO: Make all exceptions

            return DB_CONTEXT;

        }
        #region GetUserPhones
        /// <summary>
        /// Make list of specific user phone numbers.
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List with phones of user.</returns>
        public static IEnumerable<Phone> GetUserPhones(int userId) => context().Phones.ToList().Where(u => u.UserId == userId);
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
                context().Phones.Add(phone);
                context().SaveChanges();
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
            context().Phones.FirstOrDefault(p => p.Id == this.Id).PhoneNumber = newPhoneNumber;
            context().SaveChanges();
        }
        #endregion
        #region UpdateNote
        /// <summary>
        /// Update in database note of current phone number.
        /// </summary>
        /// <param name="newNote">New/edited note of phone number.</param>
        public void UpdateNote(string newNote)
        {
            context().Phones.SingleOrDefault(p => p.Id == this._id).Note = newNote;
            context().SaveChanges();
        }
        #endregion
        #region Delete
        /// <summary>
        /// Delete from database current number.
        /// </summary>
        public void Delete()
        {
            var phone = context().Phones.FirstOrDefault(p => p.Id == this.Id);
            if (phone != null)
            {
                context().Phones.Remove(phone);
                context().SaveChanges();
            }
        }
        #endregion
    }
}
