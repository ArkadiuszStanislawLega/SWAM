
using SWAM.Strings;
using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;

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
        #region Database Connection
        private static ApplicationDbContext dbContext = new ApplicationDbContext();

        protected static ApplicationDbContext context 
        {
            get
            {
                try
                {
                    return dbContext;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (DbUpdateException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (DbEntityValidationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (NotSupportedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (ObjectDisposedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (InvalidOperationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
            }
            set => dbContext = value;
        }
        #endregion
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
