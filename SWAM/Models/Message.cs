using System;
using System.Collections.Generic;

namespace SWAM.Models
{
    /// <summary>
    /// The basic class model in the database representing the message sent between two <see cref="User"/>s of this application.
    /// </summary>
    public class Message
    {
        /// <summary>
        /// Current message Id in database.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Main content of current message.
        /// </summary>
        public string ContentOfMessage { get; set; }
        /// <summary>
        /// Title of current message.
        /// </summary>
        public string TitleOfMessage { get; set; }
        /// <summary>
        /// Date of posting by sender current message.
        /// </summary>
        public DateTime PostDate { get; set; }
        /// <summary>
        /// Date of reading by receiver current message.
        /// </summary>
        public DateTime? DateOfReading { get; set; }
        /// <summary>
        /// Flag indicating current message is readed.
        /// </summary>
        public bool IsReaded { get; set; }
        /// <summary>
        /// Flag indicating current message is deleted by sender.
        /// If true - sender does not see this message in his send e-mail box.
        /// </summary>
        public bool IsDeletedBySender { get; set; }
        /// <summary>
        /// Flag indicating current message is deleted by receiver.
        /// If true - receiver does not see this message in his receive e-mail box.
        /// </summary>
        public bool IsDeletedByReceiver { get; set; }
        /// <summary>
        /// The user who sent the message.
        /// </summary>
        public User.User Sender { get; set; }
        /// <summary>
        /// The user who is the recipient of the message.
        /// </summary>
        public User.User Receiver { get; set; }

        private static readonly ApplicationDbContext DB_CONTEXT = new ApplicationDbContext();

        private static ApplicationDbContext _context
        {
            //TODO: Make all exceptions
            get
            {
                return DB_CONTEXT;
            }
        }

        public static Message GetMessage(int messageId)
        {
            throw new NotImplementedException();
           //return _context.Messages.FirstOrDefault(m => m.Id == messageId);
        }

        #region DeleteMessageBySender
        /// <summary>
        /// Change the flag _isDeletedBySender and check if the message can be removed completely from the database.
        /// </summary>
        /// <param name="messageId">Message id from database</param>
        public static void DeleteMessageBySender(int messageId)
        {
            throw new NotImplementedException();
            //if (messageId > 0)
            //{
            //    _context.Messages.FirstOrDefault(m => m.Id == messageId)._isDeletedBySender = true;
            //    _context.SaveChanges();

            //    DeleteMessage(messageId);
            //}
        }
        #endregion
        #region DeleteMessageByReceiver
        /// <summary>
        /// Change the flag _isDeletedByReceiver and check if the message can be removed completely from the database.
        /// </summary>
        /// <param name="messageId">Message id from database</param>
        public static void DeleteMessageByReceiver(int messageId)
        {
            throw new NotImplementedException();
            //if (messageId > 0)
            //{
            //    _context.Messages.FirstOrDefault(m => m.Id == messageId)._isDeletedByReceiver = true;
            //    _context.SaveChanges();

            //    DeleteMessage(messageId);
            //}
        }
        #endregion
        #region DeleteMessage
        /// <summary>
        /// Completely deletes a message from the database.
        /// </summary>
        /// <param name="messageId">Message id from database</param>
        private static void DeleteMessage(int messageId)
        {
            throw new NotImplementedException();
            //if(messageId > 0)
            //{
            //    var message = _context.Messages.FirstOrDefault(m => m.Id == messageId);
            //    if(message.IsDeletedByReceiver && message.IsDeletedBySender)
            //    {
            //        _context.Messages.Remove(message);
            //        _context.SaveChanges();
            //    }
            //}
        }
        #endregion
        #region AddManyMessages
        /// <summary>
        /// Adding list of messages to database.
        /// </summary>
        /// <param name="messages">List with mesages</param>
        public static void AddManyMessages(List<Message> messages)
        {
            throw new NotImplementedException();
            //if(messages != null)
            //{
            //    _context.Messages.AddRange(messages);
            //    _context.SaveChanges();
            //}
        }
        #endregion

        #region AddMessage
        /// <summary>
        /// Add new message to database.
        /// </summary>
        /// <param name="message">The message we want to add</param>
        public static void AddMessage(Message message)
        {
            throw new NotImplementedException();
            //if (message != null)
            //{
            //    _context.Messages.Add(message);
            //    _context.SaveChanges();
            //}
        }
        #endregion

        #region SetDateOfReading
        /// <summary>
        /// Setting date of reading of specific message to current.
        /// </summary>
        /// <param name="messageId"></param>
        public static void SetDateOfReading(int messageId)
        {
            throw new NotImplementedException();
            //_context.Messages.FirstOrDefault(m => m.Id == messageId).DateOfReading = DateTime.Now;
            //_context.SaveChanges();
        }
        #endregion
        #region SetMessageIsReaded
        /// <summary>
        /// Setting IsReaded flag in database to true.
        /// </summary>
        /// <param name="messageId">Message id from database</param>
        public static void IsReadedToTrue(int messageId)
        {
            throw new NotImplementedException();
            //if (messageId > 0)
            //{
            //    _context.Messages.FirstOrDefault(m => m.Id == messageId).IsReaded = true;
            //    _context.SaveChanges();
            //}
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
            throw new NotImplementedException();
            //if (userId > 0)
            //    return _context.Messages
            //        .Include(m => m.Receiver)
            //        .Include(m => m.Sender)
            //        .Where(m => m.Receiver.Id == userId && !m.IsReaded).ToList().Count;
            //else return -1;
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
            throw new NotImplementedException();
            //if (userId > 0)
            //    return _context.Messages
            //        .Include(m => m.Receiver)
            //        .Include(m => m.Sender)
            //        .Where(m => m.Receiver.Id == userId && !m.IsDeletedByReceiver).ToList();
            //else return null;
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
            throw new NotImplementedException();
            //if(userId > 0)
            //    return _context.Messages
            //            .Include(m => m.Receiver)
            //            .Include(m => m.Sender)
            //            .Where(m => m.Sender.Id == userId && !m.IsDeletedBySender).ToList();
            //else return null;
        }
        #endregion

    }
}
