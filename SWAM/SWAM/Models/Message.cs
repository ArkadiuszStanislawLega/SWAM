using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Models
{
    public class Message
    {
        private int _id;
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        private User _sender;
        private User _receiver;
        private string _contentOfMessage;
        private DateTime _postDate;
        private DateTime _dateOfReading;

        public int Id { get => _id; set => _id = value; }
        public User Sender { get => _sender; set => _sender = value; }
        public User Receiver { get => _receiver; set => _receiver = value; }
        public string ContentOfMessage { get => _contentOfMessage; set => _contentOfMessage = value; }
        public DateTime PostDate { get => _postDate; set => _postDate = value; }
        public DateTime DateOfReading { get => _dateOfReading; set => _dateOfReading = value; }
    }
}
