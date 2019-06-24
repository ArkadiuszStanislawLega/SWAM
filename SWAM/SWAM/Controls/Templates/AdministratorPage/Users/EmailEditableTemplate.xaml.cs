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
    using SWAM.Exceptions;
    using static SWAM.MainWindow;
    /// <summary>
    /// Logika interakcji dla klasy EmailEditableTemplate.xaml
    /// </summary>
    public partial class EmailEditableTemplate : UserControl
    {
        /// <summary>
        /// Information about actions to user.
        /// </summary>
        private string _message;

        public EmailEditableTemplate()
        {
            InitializeComponent();
        }

        #region Edit_Click
        /// <summary>
        /// Action after click edit email button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            //TODO: Make this fuction in xaml.
            TurnOff(this.Email);
            TurnOn(this.EditEmail);

            this.Confirm.IsEnabled = true;
        }
        #endregion
        #region Confirm_Click
        /// <summary>
        /// Action after click Confirm button after edit email.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is Email email)
            {
                email.UpdateEmail(EditEmail.Text);

                this._message = ($"Edytowano adress email {email.AddressEmail} użytkownikowi {email.User.Name}.");
                InformationToUser();

                //TODO: Make this in xaml
                TurnOn(this.Email);
                TurnOff(this.EditEmail);

                this.Confirm.IsEnabled = false;
            }
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
            if (DataContext is Email email)
            {
                email.Delete();

                var emailList = FindParent<EmailEditableListTemplate>(this);
                if (emailList != null) emailList.RefreshEmailsList();

                this._message = $"Usnięto adress email {email.AddressEmail}.";
                InformationToUser();
            }

        }
        #endregion

        #region InformationToUser
        /// <summary>
        /// Make information in MainWindow to user about action.
        /// </summary>
        private void InformationToUser()
        {
            try
            {
                if (SWAM.MainWindow.FindParent<SWAM.MainWindow>(this) is SWAM.MainWindow mainWindow)
                    mainWindow.InformationForUser(this._message);
                else throw new InformationLabelException(this._message);
            }
            catch (InformationLabelException ex) { ex.ShowMessage(this); }
        }
        #endregion
    }
}
