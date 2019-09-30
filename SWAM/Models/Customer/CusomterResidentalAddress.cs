﻿using System.ComponentModel.DataAnnotations.Schema;

namespace SWAM.Models.Customer
{
    [Table("CusomtersResidentalAddresses")]
    public class CusomterResidentalAddress : Address
    {
        public Customer Customer { get; set; }
    }
}