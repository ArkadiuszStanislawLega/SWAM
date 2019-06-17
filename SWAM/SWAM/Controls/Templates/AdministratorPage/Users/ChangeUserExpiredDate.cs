using SWAM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SWAM.Controls.Templates.AdministratorPage.Users
{
    class ChangeUserExpiredDate : CalendarWithButton
    {
        public ChangeUserExpiredDate()
        {
            InitializeComponent();
        }

        override protected void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            var user = DataContext as User;

            ApplicationDbContext context = new ApplicationDbContext();
            context.Users.FirstOrDefault(u => u.Id == user.Id).DateOfExpiryOfTheAccount = this.Calendar.SelectedDate;
            context.SaveChanges();

            SWAM.MainWindow.FindParent<UserProfileTemplate>(this).RefreshData();
        }
    }
}
