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
    public partial class EmailEditableListTemplate : BasicUserControl
    {
        public EmailEditableListTemplate()
        {
            InitializeComponent();
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
                var email = new Email()
                {
                    AddressEmail = this.NewEmail.Text,
                    UserId = user.Id
                };

                if (email != null)
                {
                    Email.AddEmail(email);
                    RefreshEmailsList();
                    InformationToUser($"Dodano nowy adress email {email.AddressEmail} użytkownikowi {user.Name}.");
                }
                else InformationToUser($"Nie udało się dodać użytkownikowi {user.Name} nowego adresu email.");
            }
            else InformationToUser($"Nie udało się dodać użytkownikowi nowy adress email.");
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
                var userEmails = Email.GetUserEmails(user.Id);
                try
                {
                    if (userEmails != null) Emails.ItemsSource = userEmails;
                    else throw new RefreshUserEmailListException();
                }
                catch (RefreshUserEmailListException ex) { ex.ShowMessage(this); }
            }
            else InformationToUser(ErrorMesages.REFRESH_EMAILS_LIST_ERROR, true);
        }
        #endregion
        #region CancelCreateNewEmail_Click
        /// <summary>
        /// Action after click cancel create new email button during creating new email address.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelCreateNewEmail_Click(object sender, RoutedEventArgs e) => this.NewEmail.Text = "";
        #endregion
    }
}
