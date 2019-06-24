using SWAM.Exceptions;
using SWAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SWAM.Controls.Templates.AdministratorPage
{
    /// <summary>
    /// Logika interakcji dla klasy EmailEditableListTemplate.xaml
    /// </summary>
    public partial class EmailEditableListTemplate : UserControl
    {
        /// <summary>
        /// Information to user about actions.
        /// </summary>
        private string _message;

        public EmailEditableListTemplate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Action after add new email button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewEmail_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Make this in xaml.
            SWAM.MainWindow.TurnOn(AddNewEmailContainer);
        }

        #region ConfirmNewEmail_Click
        /// <summary>
        /// Action after confirm new email click button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmNewEmail_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is User user)
            {
                //TODO: try - catch
                using (var context = new ApplicationDbContext())
                {
                    var email = new Email()
                    {
                        AddressEmail = this.NewEmail.Text,
                        UserId = user.Id
                    };

                    if (email != null)
                    {
                        context.Emails.Add(email);
                        context.SaveChanges();

                        Emails.ItemsSource = context.Emails.Where(u => u.UserId == user.Id).ToList();

                        this._message = $"Dodano nowy adress email {email.AddressEmail} użytkownikowi {user.Name}.";
                        InformationToUser();
                        //TODO: Make validations and catch exceptions - mails.
                    }
                    else
                    {
                        this._message = $"Nie udało się dodać użytkownikowi {user.Name} nowego maila.";
                        InformationToUser();
                    }
                };
            }

            //TODO: Make this function in xaml.
            SWAM.MainWindow.TurnOff(this.AddNewEmailContainer);
        }
        #endregion

        #region RefreshPhoneList
        /// <summary>
        /// Refreshing view list of emails.
        /// </summary>
        public void RefreshEmailsList()
        {
            if (DataContext is User user)
            {
                using (var context = new ApplicationDbContext())
                    Emails.ItemsSource = context.Emails.Where(u => u.UserId == user.Id).ToList();
            }
        }
        #endregion

        #region InformationToUser
        /// <summary>
        /// Changing content inforamtion label in main window.
        /// </summary>
        private bool InformationToUser(bool warning = false)
        {
            try
            {
                if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(this) is SWAM.MainWindow mainWindow)
                {
                    mainWindow.InformationForUser(this._message, warning);
                    return true;
                }
                else throw new InformationLabelException(this._message);
            }
            catch (InformationLabelException ex)
            {
                ex.ShowMessage(this);
                return false;
            }
        }
        #endregion
    }
}
