using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Models;
using SWAM.Models.Customer;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.CustomerPage
{
    /// <summary>
    /// Logika interakcji dla klasy CreateNewCustomerTemplate.xaml
    /// </summary>
    public partial class CreateNewCustomerTemplate : BasicUserControl
    {
        public CreateNewCustomerTemplate()
        {
            InitializeComponent();
            this.ResidentalAddress.ShowEditControls();
        }

        private void AddNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (EmailAddress.IsValidEmail(this.CustomerEmailAddress.Text))
            {
                Customer customer = new Customer()
                {
                    Name = this.CustomerName.Text,
                    Surname = this.CustomerSurname.Text,
                    EmailAddress = this.CustomerEmailAddress.Text,
                    Phone = this.CustomerPhone.Text,
                    ResidentalAddress = this.ResidentalAddress.GetAddress<CustomerResidentalAddress>()
                };
                Customer.Add(customer);

                CustomersListViewModel.Instance.Refresh();
                ClearTextBoxes();
            }
            else InformationToUser($"Adres email {this.CustomerEmailAddress.Text} jest błędny.");
        }

        #region ClearTextBoxes
        /// <summary>
        /// Make all TextBoxes field text clear.
        /// </summary>
        private void ClearTextBoxes()
        {
            this.CustomerName.Text = string.Empty;
            this.CustomerSurname.Text = string.Empty;
            this.CustomerEmailAddress.Text = string.Empty;
            this.CustomerPhone.Text = string.Empty;
            this.ResidentalAddress.ClearEditValues();
        }
        #endregion
    }
}
