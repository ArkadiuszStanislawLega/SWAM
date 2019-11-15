using SWAM.Strings;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;

namespace SWAM.Models.Customer
{
    [Table("CustomerResidentalAddress")]
    public class CustomerResidentalAddress
    {
        /// <summary>
        /// Number Id in database.
        /// </summary>
        public int Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string HouseNumber { get; set; }
        public string ApartmentNumber { get; set; }
        public string PostCode { get; set; }
        public Customer Customer { get; set; }
        #region DatabaseConnection
        private ApplicationDbContext _context = new ApplicationDbContext();
        private ApplicationDbContext Context
        {
            get
            {
                try
                {
                    return _context;
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
            set => _context = value;
        }
        #endregion
        public void Edit(CustomerResidentalAddress editedCustomerAddress)
        {
            if (editedCustomerAddress != null)
            {
                Context = new ApplicationDbContext();
                var customerAddress = Context.CustomerResidentalAddresses.FirstOrDefault(c => c.Id == this.Id);
                customerAddress.Country = editedCustomerAddress.Country;
                customerAddress.PostCode = editedCustomerAddress.PostCode;
                customerAddress.City = editedCustomerAddress.City;
                customerAddress.Street = editedCustomerAddress.Street;
                customerAddress.HouseNumber = editedCustomerAddress.HouseNumber;
                customerAddress.ApartmentNumber = editedCustomerAddress.ApartmentNumber;
                Context.SaveChanges();
            }
        }
    }

}
