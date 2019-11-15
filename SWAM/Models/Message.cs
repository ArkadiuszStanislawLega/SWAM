using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using SWAM.Strings;
using System.Data.Entity.Validation;

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
        public bool IsReaded { get; set; } = false;
        /// <summary>
        /// Flag indicating current message is deleted by sender.
        /// If true - sender does not see this message in his send e-mail box.
        /// </summary>
        public bool IsDeletedBySender { get; set; } = false;
        /// <summary>
        /// Flag indicating current message is deleted by receiver.
        /// If true - receiver does not see this message in his receive e-mail box.
        /// </summary>
        public bool IsDeletedByReceiver { get; set; } = false;
        /// <summary>
        /// The user who sent the message.
        /// </summary>
        public User.User Sender { get; set; }
        /// <summary>
        /// The user who is the recipient of the message.
        /// </summary>
        public User.User Receiver { get; set; }

        #region Database connection
        private static ApplicationDbContext dbContext = new ApplicationDbContext();
        private static ApplicationDbContext Context
        {
            get
            {
                try
                {
                    return dbContext;
                }
                catch (DbUpdateConcurrencyException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (DbUpdateException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (DbEntityValidationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (NotSupportedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (ObjectDisposedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
                catch (InvalidOperationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                    return null;
                }
            }
            set => dbContext = value;
        }
        #endregion
        public static Message GetMessage(int messageId) => Context.Messages.FirstOrDefault(m => m.Id == messageId);
        

        #region DeleteMessageBySender
        /// <summary>
        /// Change the flag _isDeletedBySender and check if the message can be removed completely from the database.
        /// </summary>
        /// <param name="message">Message id from database</param>
        public static void DeleteMessageBySender(Message message)
        {
            if (message != null)
            {
                Context.People
                    .OfType<User.User>()
                    .FirstOrDefault(u => u.Id == message.Sender.Id)
                    .Messages.Single(m => m.Id == message.Id)
                    .IsDeletedBySender = true;

                if (Context.SaveChanges() == 1)
                {
                    message.IsDeletedBySender = true;

                    if (message.IsDeletedBySender && message.IsDeletedByReceiver)
                        DeleteMessage(message);
                }
            }
        }
        #endregion
        #region DeleteMessageByReceiver
        /// <summary>
        /// Change the flag _isDeletedByReceiver and check if the message can be removed completely from the database.
        /// </summary>
        /// <param name="messageId">Message id from database</param>
        public static void DeleteMessageByReceiver(Message message)
        {
            if (message != null)
            {
                Context.People
                    .OfType<User.User>()
                    .FirstOrDefault(u => u.Id == message.Sender.Id)
                    .Messages.Single(m => m.Id == message.Id)
                    .IsDeletedByReceiver = true;

                if (Context.SaveChanges() == 1)
                {
                    message.IsDeletedBySender = true;

                    if (message.IsDeletedBySender && message.IsDeletedByReceiver)
                        DeleteMessage(message);
                }
            }
        }
        #endregion
        #region DeleteMessage
        /// <summary>
        /// Completely deletes a message from the database.
        /// </summary>
        /// <param name="message">Message id from database</param>
        private static void DeleteMessage(Message message)
        {
            if (message != null && message.Sender.Id > 0)
            {
                Context.People
                    .OfType<User.User>()
                    .FirstOrDefault(u => u.Id == message.Sender.Id)
                    .Messages.Remove(message);

                Context.SaveChanges();
            }
        }
        #endregion
        #region AddManyMessages
        /// <summary>
        /// Adding list of messages to database.
        /// </summary>
        /// <param name="messages">List with mesages</param>
        public static void AddManyMessages(List<Message> messages)
        {
            if (messages != null)
            {
                Context = new ApplicationDbContext();
                foreach (var message in messages)
                {
                    var sender = Context.People
                        .OfType<User.User>()
                        .FirstOrDefault(u => u.Id == message.Sender.Id);

                    var receiver = Context.People
                        .OfType<User.User>()
                        .FirstOrDefault(u => u.Id == message.Receiver.Id);

                    if (receiver != null & sender != null)
                    {
                        Context.Messages.Add(new Message()
                        {
                            Sender = sender,
                            Receiver = receiver,
                            TitleOfMessage = message.TitleOfMessage,
                            ContentOfMessage = message.ContentOfMessage,
                            PostDate = DateTime.Now
                        });
                    }
                }
                Context.SaveChanges();
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
                Context = new ApplicationDbContext();
                var sender = Context.People
                                     .OfType<User.User>()
                                     .FirstOrDefault(u => u.Id == message.Sender.Id);

                var receiver = Context.People
                                       .OfType<User.User>()
                                       .FirstOrDefault(u => u.Id == message.Receiver.Id);
                if (receiver != null & sender != null)
                {
                    Context.Messages.Add(new Message()
                    {
                        Sender = sender,
                        Receiver = receiver,
                        TitleOfMessage = message.TitleOfMessage,
                        ContentOfMessage = message.ContentOfMessage,
                        PostDate = DateTime.Now
                    });
                    Context.SaveChanges();
                }
            }
        }
        #endregion

        #region SetDateOfReading
        /// <summary>
        /// Setting date of reading of specific message to current.
        /// </summary>
        /// <param name="messageId"></param>
        /// <returns>True if the property DateOfReading has been changed to current date and time correctly in the database.</returns>
        public static bool SetDateOfReading(Message message)
        {
            if (message != null)
            {
                Context.People.OfType<User.User>().FirstOrDefault(u => u.Id == message.Sender.Id).Messages.Single(m => m.Id == message.Id).DateOfReading = DateTime.Now;
                if (Context.SaveChanges() == 1)
                    return true;
            }

            return false;
        }
        #endregion
        #region SetMessageIsReaded
        /// <summary>
        /// Setting IsReaded flag in database to true.
        /// </summary>
        /// <param name="messageId">Message id from database</param>
        /// <returns>True if the property IsReaded has been changed to true correctly in the database.</returns>
        public static bool IsReadedToTrue(Message message)
        {
            if (message != null)
            {
                Context.People
                 .OfType<User.User>()
                 .FirstOrDefault(u => u.Id == message.Sender.Id)
                 .Messages.Single(m => m.Id == message.Id)
                 .IsReaded = true;

                if (Context.SaveChanges() == 1)
                    return true;
            }

            return false;
        }
        #endregion
        #region CountUnreadedMessages
        /// <summary>
        /// Counting number of unreaded message of specific user id.
        /// </summary>
        /// <param name="user">User we want to check</param>
        /// <returns>Number of specific unreaded messages. If number is below 0 user Id is incorrect.</returns>
        public static int CountUnreadedMessages(User.User user)
        {
            if (user != null)
            {
                Context = new ApplicationDbContext();
                return Context.Messages
                    .Include(m => m.Receiver)
                    .Include(m => m.Sender)
                    .Where(m => m.Receiver.Id == user.Id && !m.IsReaded).ToList().Count;
            }
             
            return -1;
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
                return Context.Messages
                    .Include(m => m.Receiver)
                    .Include(m => m.Sender)
                    .Where(m => m.Receiver.Id == userId && !m.IsDeletedByReceiver)
                    .ToList();
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
            if (userId > 0)
                return Context.Messages
                        .Include(m => m.Receiver)
                        .Include(m => m.Sender)
                        .Where(m => m.Sender.Id == userId && !m.IsDeletedBySender).ToList();
            else return null;
        }
        #endregion
    }
}
