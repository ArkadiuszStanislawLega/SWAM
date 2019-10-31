using SWAM.Controls.Templates.AdministratorPage;
using System.Windows.Input;

namespace SWAM.Commands.AdministratorPage
{
    public static class UserProfileCommands
    {
        public static readonly RoutedUICommand EditUserName = new RoutedUICommand("Edit user name", "EditUserName", typeof(UserProfileTemplate));
        public static readonly RoutedUICommand EditUserPermissions = new RoutedUICommand("Edit user permisssions", "EditUserPermissions", typeof(UserProfileTemplate));
        public static readonly RoutedUICommand EditUserPassword = new RoutedUICommand("Edit user password", "EditUserPassword", typeof(UserProfileTemplate));
    }
}
