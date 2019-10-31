using System.Windows.Input;

namespace SWAM.Commands.MainWindow
{
    public static class MainWindowCommands
    {
        public static readonly RoutedUICommand RefreshData = new RoutedUICommand("Refresh data", "RefreshData", typeof(SWAM.MainWindow));
    }
}
