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
        private bool _isDeletedBySender;
        private bool _isDeletedByReceiver;

        public int Id { get => _id; set => _id = value; }
        public User Sender { get => _sender; set => _sender = value; }
        public User Receiver { get => _receiver; set => _receiver = value; }
        public string ContentOfMessage { get => _contentOfMessage; set => _contentOfMessage = value; }
        public string TitleOfMessage { get => _titleOfMessage; set => _titleOfMessage = value; }
        public DateTime PostDate { get => _postDate; set => _postDate = value; }
        public DateTime? DateOfReading { get => _dateOfReading; set => _dateOfReading = value; }
        public bool IsReaded { get => _isReaded; set => _isReaded = value; }
        public bool IsDeletedBySender { get => _isDeletedBySender; set => _isDeletedBySender = value; }
        public bool IsDeletedByReceiver { get => _isDeletedByReceiver; set => _isDeletedByReceiver = value; }

        private static readonly ApplicationDbContext DB_CONTEXT = new ApplicationDbContext();

        private static ApplicationDbContext _context
        {
            //TODO: Make all exceptions
            get
            {
                return DB_CONTEXT;
            }
        }

        public static Message GetMessage(int messageId) => _context.Messages.FirstOrDefault(m => m.Id == messageId);

        public static void DeleteMessage(int messageId)
        {
            if (messageId > 0)
            {
                _context.Messages.Remove(GetMessage(messageId));
                _context.SaveChanges();
            }
        }

        #region AddManyMessages
        /// <summary>
        /// Adding list of messages to database.
        /// </summary>
        /// <param name="messages">List with mesages</param>
        public static void AddManyMessages(List<Message> messages)
        {
            if(messages != null)
            {
                _context.Messages.AddRange(messages);
                _context.SaveChanges();
            }
        }
        #endregion
        #region AddMessage
        /// <summary>
        /// Add new message to database.
        /// </summary>
        /// <param name="message">The message we want to add</param>
        public static void AddMessage(Message message)
        {
            if (message != null)
            {
                _context.Messages.Add(message);
                _context.SaveChanges();
            }
        }
        #endregion

        #region SetDateOfReading
        /// <summary>
        /// Setting date of reading of specific message to current.
        /// </summary>
        /// <param name="messageId"></param>
        public static void SetDateOfReading(int messageId)
        {
            _context.Messages.FirstOrDefault(m => m.Id == messageId).DateOfReading = DateTime.Now;
            _context.SaveChanges();
        }
        #endregion
        #region SetMessageIsReaded
        /// <summary>
        /// Setting IsReaded flag in database to true.
        /// </summary>
        /// <param name="messageId">Message id from database</param>
        public static void IsReadedToTrue(int messageId)
        {
            if (messageId > 0)
            {
                _context.Messages.FirstOrDefault(m => m.Id == messageId).IsReaded = true;
                _context.SaveChanges();
            }
        }
        #endregion
        #region CountUnreadedMessages
        /// <summary>
        /// Counting number of unreaded message of specific user id.
        /// </summary>
        /// <param name="userId">Id number of the user we want to check</param>
        /// <returns>Number of specific unreaded messages. If number is below 0 user Id is incorrect.</returns>
        public static int CountUnreadedMessages(int userId)
        {
            if (userId > 0)
                return _context.Messages
                    .Include(m => m.Receiver)
                    .Include(m => m.Sender)
                    .Where(m => m.Receiver.Id == userId && !m.IsReaded).ToList().Count;
            else return -1;
        }
        #endregion

        #region AllReceivedMessages
        /// <summary>
        /// All received messages from database that the user has received.
        /// </summary>
        /// <param name="userId">Id number of the user we want to get list</param>
        /// <returns>List with all received messages</returns>
        public static IList<Message> AllReceivedMessages(int userId)
        {
            if (userId > 0)
                return _context.Messages
                    .Include(m => m.Receiver)
                    .Include(m => m.Sender)
                    .Where(m => m.Receiver.Id == userId).ToList();
            else return null;
        }
        #endregion
        #region AllSendedMessages
        /// <summary>
        /// All sended messages from database that the user has send.
        /// </summary>
        /// <param name="userId>Id number of the user we want to get list</param>
        /// <returns>List with all sended messages</returns>
        public static IList<Message> AllSendedMessages(int userId)
        {
            if(userId > 0)
                return _context.Messages
                        .Include(m => m.Receiver)
                        .Include(m => m.Sender)
                        .Where(m => m.Sender.Id == userId).ToList();
            else return null;
        }
        #endregion

    }
}
