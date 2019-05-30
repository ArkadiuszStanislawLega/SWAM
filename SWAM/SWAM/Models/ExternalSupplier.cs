﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
    public class ExternalSupplier
    {
        int _id;
        string _name;
        /// <summary>
        /// Tax Identification Number
        /// </summary>
        string _tin;
        Address _address;
        IList<Phone> _phones;

        public int Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public Address Address { get => _address; set => _address = value; }
        public IList<Phone> Phone { get => _phones; set => _phones = value; }
        public string Tin { get => _tin; set => _tin = value; }
    }
}
