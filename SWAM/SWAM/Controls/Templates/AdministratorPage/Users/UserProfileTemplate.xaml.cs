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
    /// Logika interakcji dla klasy UserProfileTemplate.xaml
    /// </summary>
    public partial class UserProfileTemplate : UserControl
    {
 
        public UserProfileTemplate()
        {
            InitializeComponent();

        }

        private void NewCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }


        private void NewCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            if (ShowDateOfUserBlock.Text == "")
                ConfirmUserBlockadeData.Stop();

            if (ShowDateOfUserBlock.Text == "Kliknięte")
            {
                ShowDateOfUserBlock.Text = "Kliknięte";
                ConfirmUserBlockadeData.Begin();
            }
        }

        private void EditUser_Click(object sender, RoutedEventArgs e)
        {
            TurnOffValues();
            TurnOnEditFields();

            SWAM.MainWindow.TurnOn(this.ConfirmChanges);
        }

        #region TurnOnEditFields
        /// <summary>
        /// Switch on all fields required to change user profile values.
        /// </summary>
        private void TurnOnEditFields()
        {
            SWAM.MainWindow.TurnOn(this.EditName);
            SWAM.MainWindow.TurnOn(this.EditPassword);
            SWAM.MainWindow.TurnOn(this.EditConfirmPassword);
            SWAM.MainWindow.TurnOn(this.EditPermissions);
            //TurnOn(this.EditEmailAddress);
        }
        #endregion
        #region TurnOffValues
        /// <summary>
        /// Turn off all uneditables values downloaded from database.
        /// </summary>
        private void TurnOffValues()
        {
            SWAM.MainWindow.TurnOff(this.Name);
            SWAM.MainWindow.TurnOff(this.Password);
            SWAM.MainWindow.TurnOff(this.Permissions);
            //TurnOff(this.EmailAddress);
        }
        #endregion

        private void ConfirmChanges_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeletUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BlockUser_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditDateOfUserBlock_Click(object sender, RoutedEventArgs e)
        {
            UserBlockCallendar.IsEnabled = true;
            UserBlockCallendar.Visibility = Visibility;
        }
    }
}
