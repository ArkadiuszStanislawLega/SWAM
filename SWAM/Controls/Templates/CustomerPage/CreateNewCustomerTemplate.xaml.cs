using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Models;
using SWAM.Models.Customer;
using System.Linq;
using System.Text.RegularExpressions;
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

                InformationToUser($"Dodano nowego klienta - {this.CustomerName.Name}");

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
        #region CustomerPhone_TextChanged
        /// <summary>
        /// User can write only numbers.
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">New char in text field event</param>
        private void CustomerPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            char[] charArray;

            Regex regex = new Regex("[^0-9]+");
            if (sender is TextBox textBox && regex.IsMatch(textBox.Text))
            {
                var values = textBox.Text.ToList();
                values.RemoveAt(values.Count - 1);

                charArray = values.ToArray();

                textBox.Text = new string(charArray);
                InformationToUser($"Podając tą wartość możesz użyć tylko cyfr.", true);
            }
            else InformationToUser("");
        }
        #endregion
    }
}
