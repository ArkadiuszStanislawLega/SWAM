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
    /// Logika interakcji dla klasy PhoneNumbersEditableListTemplate.xaml
    /// </summary>
    public partial class PhoneNumbersEditableListTemplate : UserControl
    {
        public PhoneNumbersEditableListTemplate()
        {
            InitializeComponent();
        }

        private void AddNewPhone_Click(object sender, RoutedEventArgs e)
        {
            TurnOn(this.AddNewPhoneContainer);
        }

        private void ConfirmNewPhone_Click(object sender, RoutedEventArgs e)
        {
            User user = DataContext as User;

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var phone = new Phone()
                {
                    PhoneNumber = this.NewPhone.Text,
                    Note = this.NewPhoneNote.Text,
                    User = user
                };


                if (phone != null)
                {
                    context.Phones.Add(phone);
                    //this.Information.Content = "Udało się dodać użytkownika " + user.Name;
                }
                else
                {
                    //this.Information.Content = "Nie udało się dodać użytkownika " + user.Name;
                    //this.Information.Background = this.FindResource("WhiteCream") as Brush;
                }
                context.SaveChanges();
            };
            SWAM.MainWindow.FindParent<UsersControlPanelTemplate>(this).RefreshUsersList();
        }


        private void TurnOff(FrameworkElement userControl)
        {
            userControl.IsEnabled = false;
            userControl.Visibility = Visibility.Collapsed;
        }

        private void TurnOn(FrameworkElement userControl)
        {
            userControl.IsEnabled = true;
            userControl.Visibility = Visibility.Visible;
        }
    }
}
