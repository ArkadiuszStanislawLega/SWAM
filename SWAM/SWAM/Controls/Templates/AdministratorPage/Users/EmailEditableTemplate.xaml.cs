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
    using static SWAM.MainWindow;
    /// <summary>
    /// Logika interakcji dla klasy EmailEditableTemplate.xaml
    /// </summary>
    public partial class EmailEditableTemplate : UserControl
    {
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
            var email = DataContext as Email;

            email.UpdateEmail(EditEmail.Text);
            FindParent<SWAM.MainWindow>(this).InformationForUser($"Edytowano adress email {email.AddressEmail} użytkownikowi {email.User.Name}.");

            //TODO: Make this function in xaml
            TurnOn(this.Email);
            TurnOff(this.EditEmail);

            this.Confirm.IsEnabled = false;
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
            var email = DataContext as Email;
            email.Delete();

            FindParent<EmailEditableListTemplate>(this).RefreshEmailsList();
            FindParent<SWAM.MainWindow>(this).InformationForUser($"Usnięto adress email {email.AddressEmail}.");

        }
        #endregion
    }
}
