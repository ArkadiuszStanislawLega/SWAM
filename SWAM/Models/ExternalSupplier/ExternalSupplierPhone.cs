using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace SWAM.Models.ExternalSupplier
{
    [Table("ExternalSuppliersPhones")]
    public class ExternalSupplierPhone
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


        public ExternalSupplier ExternalSupplier { get; set; }


        private static ApplicationDbContext dbContext = new ApplicationDbContext();

        private static ApplicationDbContext _context
        {
            //TODO: Make all exceptions
            get
            {
                return dbContext;
            }
            set => dbContext = value;
        }

        #region UpdateNumber
        /// <summary>
        /// Update current phone number.
        /// </summary>
        /// <param name="newPhoneNumber">New phone/edited number.</param>
        /// <returns>True if update of phone number is corectly edited.</returns>
        public bool UpdateNumber(ExternalSupplierPhone externalSupplierPhone, string newPhoneNumber)
        {
            if (externalSupplierPhone != null)
            {
                if (externalSupplierPhone.PhoneNumber == newPhoneNumber)
                    return true;

                _context.ExternalSupplierPhones.FirstOrDefault(e => e.Id == externalSupplierPhone.Id).PhoneNumber = newPhoneNumber;

                if (_context.SaveChanges() == 1)
                    return true;
            }

            return false;
        }
        #endregion
        #region UpdateNote
        /// <summary>
        /// Update in database note of current phone number.
        /// </summary>
        /// <param name="newNote">New/edited note of phone number.</param>
        /// <returns>True if update of phone number is corectly edited.</returns>
        public bool UpdateNote(ExternalSupplierPhone externalSupplierPhone, string newNote)
        {
            if (externalSupplierPhone != null)
            {
                if (externalSupplierPhone.Note == newNote)
                    return true;

                _context.ExternalSupplierPhones.FirstOrDefault(e => e.Id == externalSupplierPhone.Id).Note = newNote;

                if (_context.SaveChanges() == 1)
                    return true;
            }

            return false;
        }
        #endregion
        #region RemoveFromDb
        /// <summary>
        /// Remove current number from database.
        /// </summary>
        /// <returns>True if phone is correctly removed from database.</returns>
        public bool RemoveFromDb()
        {
            _context = new ApplicationDbContext();

            if(_context.ExternalSupplierPhones.FirstOrDefault(u => u.Id == this.Id) is ExternalSupplierPhone phone)
            {
                _context.ExternalSupplierPhones.Remove(phone);
                //Two rows are affected in database.
                //One in Phone table and one in UserPhone table.
                if (_context.SaveChanges() == 2)
                    return true;
            }

            return false;
        }
        #endregion
        #region Get
        /// <summary>
        /// Retrieves the phone number from the database.
        /// </summary>
        /// <param name="id">External supplier phone id in database.</param>
        /// <returns>Specific full external supplier phone from database.</returns>
        public ExternalSupplierPhone Get(int id)
        {
            _context = new ApplicationDbContext();
            return _context.ExternalSupplierPhones.Include(e => e.ExternalSupplier).FirstOrDefault(e => e.Id == id);
        }
        #endregion
    }
}
