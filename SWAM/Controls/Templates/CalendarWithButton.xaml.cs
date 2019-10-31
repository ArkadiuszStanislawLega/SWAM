using SWAM.Models;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Input;


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

        virtual protected void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
               // Here type action.
        }
    }
}
