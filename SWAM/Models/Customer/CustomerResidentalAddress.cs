using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
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

        private ApplicationDbContext _context = new ApplicationDbContext();
        private ApplicationDbContext context
        {
            //TODO: Try catch block.
            get => this._context;
            set => this._context = value;
        }
        public void Edit(CustomerResidentalAddress editedCustomerAddress)
        {
            if (editedCustomerAddress != null)
            {
                context = new ApplicationDbContext();
                var customerAddress = context.CustomerResidentalAddresses.FirstOrDefault(c => c.Id == this.Id);
                customerAddress.Country = editedCustomerAddress.Country;
                customerAddress.PostCode = editedCustomerAddress.PostCode;
                customerAddress.City = editedCustomerAddress.City;
                customerAddress.Street = editedCustomerAddress.Street;
                customerAddress.HouseNumber = editedCustomerAddress.HouseNumber;
                customerAddress.ApartmentNumber = editedCustomerAddress.ApartmentNumber;
                context.SaveChanges();
            }
        }
    }

}
