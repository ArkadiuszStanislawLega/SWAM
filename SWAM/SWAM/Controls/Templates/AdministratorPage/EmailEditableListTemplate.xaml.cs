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

        private void AddNewEmail_Click(object sender, RoutedEventArgs e)
        {
            SWAM.MainWindow.TurnOn(AddNewEmailContainer);
        }

        private void ConfirmNewEmail_Click(object sender, RoutedEventArgs e)
        {
            User user = DataContext as User;

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var email = new Email()
                {
                    AddressEmail = this.NewEmail.Text,
                    UserId = user.Id
                };

                if (email != null)
                {
                    context.Emails.Add(email);
                    //this.Information.Content = "Udało się dodać użytkownika " + user.Name;
                }
                else
                {
                    //this.Information.Content = "Nie udało się dodać użytkownika " + user.Name;
                    //this.Information.Background = this.FindResource("WhiteCream") as Brush;
                }
                context.SaveChanges();
            };
        }
    }
}
