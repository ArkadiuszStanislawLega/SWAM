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
using SWAM;

namespace SWAM.Controls.Templates.AdministratorPage
{
    /// <summary>
    /// Logika interakcji dla klasy CreateNewUserTemplate.xaml
    /// </summary>
    public partial class CreateNewUserTemplate : UserControl
    {
        public CreateNewUserTemplate()
        {
            InitializeComponent();
        }

        private void Comfirm_Click(object sender, RoutedEventArgs e)
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                int lastId = 0;
                try
                {
                    lastId = context.Users.Max(u => u.Id);
                }catch (InvalidOperationException) { }

                var user = new User()
                {
                    Id = lastId++,
                    Name = this.NewUserName.Text,
                    Password = this.UserPassword.Password,
                    Permissions = (Enumerators.UserType)this.UserPermissions.SelectedValue
                };

                if (user != null)
                {
                    context.Users.Add(user);
                    this.Information.Content = "Udało się dodać użytkownika " + user.Name;
                }
                else
                {
                    this.Information.Content = "Nie udało się dodać użytkownika " + user.Name;
                    this.Information.Background = this.FindResource("WhiteCream") as Brush;
                }
                context.SaveChanges();
                SWAM.MainWindow.FindParent<UsersControlPanelTemplate>(this).RefreshUsersList();
            }
          
        }
    }
}
