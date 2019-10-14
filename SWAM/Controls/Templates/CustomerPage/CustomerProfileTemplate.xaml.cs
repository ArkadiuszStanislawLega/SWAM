﻿using System.Windows.Controls;

namespace SWAM.Controls.Templates.CustomerPage
{
    /// <summary>
    /// Logika interakcji dla klasy CustomerProfile.xaml
    /// </summary>
    public partial class CustomerProfileTemplate : UserControl
    {
        public CustomerProfileTemplate()
        {
            InitializeComponent();

            this.Name.ConfirmChangeName.Click += ConfirmChangeName_Click;
            this.Surname.ConfirmChangeSurname.Click += ConfirmChangeSurname_Click;
            this.Phone.ConfirmChangePhone.Click += ConfirmChangePhone_Click;
            this.EmailAddress.ConfirmChangeEmailAddress.Click += ConfirmChangeEmailAddress_Click;
        }
        private void ConfirmChangeName_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ConfirmChangeSurname_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ConfirmChangePhone_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ConfirmChangeEmailAddress_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }


        private void ChangeDeliveryAddress_Click(object sender, System.Windows.RoutedEventArgs e) => DeliveryAddress.ShowEditControls();

        private void ChangeResidentalAddress_Click(object sender, System.Windows.RoutedEventArgs e) => ResidentalAddress.ShowEditControls();

        private void StackPanel_ManipulationInertiaStarting(object sender, System.Windows.Input.ManipulationInertiaStartingEventArgs e)
        {

        }
    }
}
