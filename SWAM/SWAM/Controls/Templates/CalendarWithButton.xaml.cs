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

namespace SWAM.Controls.Templates
{
    /// <summary>
    /// Logika interakcji dla klasy CalendarWithButton.xaml
    /// </summary>
    public partial class CalendarWithButton : UserControl
    {
        public CalendarWithButton()
        {
            InitializeComponent();
        }

        protected void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        virtual protected void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
               // Here type action.
        }
    }
}
