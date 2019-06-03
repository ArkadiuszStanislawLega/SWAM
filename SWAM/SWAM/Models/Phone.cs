﻿using System;
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
        [Key]
        [ForeignKey("User")]
        int _userId;
        string _note;
        string _phoneNumber;

        public int Id { get => _id; set => _id = value; }
        public int UserId { get => _userId; set => _userId = value; }
        public string PhoneNumber { get => _phoneNumber; set => _phoneNumber = value; }
        public string Note { get => _note; set => _note = value; }
    }
}
