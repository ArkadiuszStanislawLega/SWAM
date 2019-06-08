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
        public EmailEditableListTemplate()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Action after add new email button click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddNewEmail_Click(object sender, RoutedEventArgs e) => SWAM.MainWindow.TurnOn(AddNewEmailContainer);

        #region ConfirmNewEmail_Click
        /// <summary>
        /// Action after confirm new email click button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ConfirmNewEmail_Click(object sender, RoutedEventArgs e)
        {
            User user = DataContext as User;

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var email = new Email()
                {
                    AddressEmail = this.NewEmail.Text,
                    User = user
                };

                if (email != null)
                {
                    context.Emails.Add(email);
                    //TODO: Make validations and catch exceptions - mails.
                    //this.Information.Content = "Udało się dodać użytkownika " + user.Name;
                }
                else
                {
                    //this.Information.Content = "Nie udało się dodać użytkownika " + user.Name;
                    //this.Information.Background = this.FindResource("WhiteCream") as Brush;
                }
                context.SaveChanges();
            };

            SWAM.MainWindow.TurnOff(this.AddNewEmailContainer);

            RefreshEmailsList();

        }
        #endregion

        #region RefreshPhoneList
        /// <summary>
        /// Refreshing view list of emails.
        /// </summary>
        public void RefreshEmailsList()
        {
            var user = DataContext as User;
            Emails.ItemsSource = user.Emails;
        }
        #endregion
    }
}
