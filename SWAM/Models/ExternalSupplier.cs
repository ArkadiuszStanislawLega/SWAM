using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace SWAM.Models
{
    /// <summary>
    /// The basic model of the class in the database representing the external supplier to the warehouses.
    /// </summary>
    public class ExternalSupplier : Person
    {
        /// <summary>
        /// Tax Identification Number.
        /// </summary>
        public string Tin { get; set; }
        /// <summary>
        /// External supplier's office address.
        /// </summary>
        public Address Address { get; set; }
        /// <summary>
        /// External supplier's phone numbers.
        /// </summary>
        public IList<Phone> Phones { get; set; } 
    }
}
