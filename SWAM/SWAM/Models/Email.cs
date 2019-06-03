using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
    public class Email
    {
        int _id;
        [Key]
        [ForeignKey("User")]
        int _userId;
        string _addressEmail;

        public int Id { get => _id; set => _id = value; }
        public int UserId { get => _userId; set => _userId = value; }
        public string AddressEmail { get => _addressEmail; set => _addressEmail = value; }
    }
}
