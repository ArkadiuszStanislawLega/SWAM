﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SWAM.Models.Customer
{
    [Table("CustomerOrderDeliveryAddress")]
    public class CustomerOrderDeliveryAddress
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
        public CustomerOrder CustomerOrder { get; set; }
    }
}
