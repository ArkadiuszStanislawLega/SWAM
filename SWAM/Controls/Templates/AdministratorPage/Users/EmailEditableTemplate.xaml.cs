using SWAM.Models;
using System.Windows;
using SWAM.Strings;
using SWAM.Exceptions;
using SWAM.Models.User;

namespace SWAM.Controls.Templates.AdministratorPage
{
    using static SWAM.MainWindow;
    /// <summary>
    /// Logika interakcji dla klasy EmailEditableTemplate.xaml
    /// </summary>
    public partial class EmailEditableTemplate : BasicUserControl
    {
        /// <summary>
        /// Email address before edit.
        /// </summary>
        private string _emailAddress;
        public EmailEditableTemplate()
        {
            InitializeComponent();
        }

        #region Confirm_Click
        /// <summary>
        /// Action after click Confirm button after edit email.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            //Make sure that datacontext is email
            if (DataContext is UserEmailAddress email)
            {
                //Make sure confirm window is not null and is ready to show message for user.
                if (this._confirmWindow != null)
                {
                    //Show confirmation window about changes.
                    this._confirmWindow.Show($"Potwierdź zmianę adresu email na {this.EditEmail.Text}?", out bool isConfirmed, "Potwierdź dokonanie zmiany");
                    //If user confirmed in dialog window changes...
                    if (isConfirmed)
                    {
                        //Update email in database and inform user about it.
                        if (email.User.ChangeEmailAddress(email, EditEmail.Text)) InformationToUser($"Edytowano adress email {email.AddressEmail} użytkownikowi {email.User.Name}.");
                        else InformationToUser($"{ErrorMesages.DURING_EDIT_EMAIL_ERROR} {ErrorMesages.DATABASE_ERROR}", true);
                    }
                    else InformationToUser($"{ErrorMesages.DURING_EDIT_EMAIL_ERROR} {ErrorMesages.MESSAGE_WINDOW_ERROR}", true);
                }
                else InformationToUser($"{ErrorMesages.DURING_EDIT_EMAIL_ERROR} {ErrorMesages.CONFIRMATION_WINDOW_ERROR}", true);
            }
            else InformationToUser($"{ErrorMesages.DURING_EDIT_EMAIL_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion
        #region Delete_Click
        /// <summary>
        /// Action after click delete email button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            //Make sure that datacontext is email
            if (DataContext is UserEmailAddress userEmailAddress)
            {
                //Make sure confirm window is not null and is ready to show message for user.
                if (this._confirmWindow != null)
                {
                    //Show confirmation window about changes.
                    this._confirmWindow.Show($"Czy na pewno chcesz usunąc adres email {userEmailAddress.AddressEmail}?", out bool isConfirmed, "Potwierdź usunięcie adresu email");
                    //If user confirmed in dialog window changes...
                    if (isConfirmed)
                    {
                        //Delete email in database and inform user about it.
                        if (userEmailAddress.Delete())
                        {
                            InformationToUser($"Usunięto adress email {userEmailAddress.AddressEmail}.");
                            try
                            {
                                //Try refresh list with emails in user profile.
                                var emailList = FindParent<EmailEditableListTemplate>(this);
                                if (emailList != null) emailList.RefreshEmailsList();
                                else throw new RefreshUserEmailListException();
                            }
                            catch (RefreshUserEmailListException ex) { ex.ShowMessage(this); };
                        }
                        else InformationToUser($"{ErrorMesages.DURIGN_DELETE_EMAIL_ERROR} {ErrorMesages.DATABASE_ERROR}", true);

                    }
                }
                else InformationToUser($"{ErrorMesages.DURIGN_DELETE_EMAIL_ERROR} {ErrorMesages.MESSAGE_WINDOW_ERROR}", true);
            }
            else InformationToUser($"{ErrorMesages.DURIGN_DELETE_EMAIL_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion
        #region Cancel_Click
        /// <summary>
        /// Action after click cancel button during edit addres email.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Email.Text = this._emailAddress;
            this.EditEmail.Text = this._emailAddress;
        }
        #endregion
        #region Edit_Click
        /// <summary>
        /// Make instance with email address before edit it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e) => this._emailAddress = Email.Text;
        #endregion
    }
}
