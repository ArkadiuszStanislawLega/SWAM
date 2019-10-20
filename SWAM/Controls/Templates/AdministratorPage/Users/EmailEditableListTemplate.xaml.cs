using SWAM.Exceptions;
using SWAM.Models;
using SWAM.Models.User;
using System.Windows;
using SWAM.Strings;
using System.Data.Entity;
using System.Linq;
using System.Collections.Generic;

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
                if (EmailAddress.IsValidEmail(this.NewEmail.Text))
                {
                    var email = new UserEmailAddress()
                    {
                        AddressEmail = this.NewEmail.Text
                        //TODO: Add edit note
                    };

                    if (email != null)
                    {

                        user.AddAddressEmail(email);
                        this.NewEmail.Text = string.Empty;

                        RefreshEmailsList();
                        InformationToUser($"Dodano nowy adress email {email.AddressEmail} użytkownikowi {user.Name}.");
                    }
                    else InformationToUser($"Nie udało się dodać użytkownikowi {user.Name} nowego adresu email.");
                }
                else InformationToUser($"Adres {this.NewEmail.Text} jest błędny.", true);
            }
            else InformationToUser($"Nie udało się dodać użytkownikowi nowy adress email.");

            this.NewEmail.Text = string.Empty;
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
                if (user.GetEmailsAddresses() is List<UserEmailAddress> list)
                    Emails.ItemsSource = list;
                else
                    InformationToUser($"{ErrorMesages.REFRESH_EMAILS_LIST_ERROR} {ErrorMesages.DATABASE_ERROR}", true);
            }
            else
                InformationToUser(ErrorMesages.REFRESH_EMAILS_LIST_ERROR, true);
        }
        #endregion
        #region CancelCreateNewEmail_Click
        /// <summary>
        /// Action after click cancel create new email button during creating new email address.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelCreateNewEmail_Click(object sender, RoutedEventArgs e) => this.NewEmail.Text = string.Empty;
        #endregion
    }
}
