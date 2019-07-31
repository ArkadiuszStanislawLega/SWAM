using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;

namespace SWAM.Models
{
    public class Message
    {
        private int _id;
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }

        private User _sender;
        private User _receiver;
        private string _titleOfMessage;
        private string _contentOfMessage;
        private DateTime _postDate;
        private DateTime? _dateOfReading;
        private bool _isReaded;

        public int Id { get => _id; set => _id = value; }
        public User Sender { get => _sender; set => _sender = value; }
        public User Receiver { get => _receiver; set => _receiver = value; }
        public string ContentOfMessage { get => _contentOfMessage; set => _contentOfMessage = value; }
        public string TitleOfMessage { get => _titleOfMessage; set => _titleOfMessage = value; }
        public DateTime PostDate { get => _postDate; set => _postDate = value; }
        public DateTime? DateOfReading { get => _dateOfReading; set => _dateOfReading = value; }
        public bool IsReaded { get => _isReaded; set => _isReaded = value; }

        private static readonly ApplicationDbContext DB_CONTEXT = new ApplicationDbContext();

        private static ApplicationDbContext _context
        {
            //TODO: Make all exceptions
            get
            {
                return DB_CONTEXT;
            }
        }

        public static IList<Message> AllMessages(int userId)
        {
            if (userId > 0)
                return _context.Messages
                    .Include(m => m.Receiver)
                    .Include(m => m.Sender)
                    .Where(m => m.Receiver.Id == userId).ToList();
            else return null;
        }

        public static void AddMessage(Message message)
        {
            if (message != null)
            {
                _context.Messages.Add(message);
                _context.SaveChanges();
            }
        }
    }
}
