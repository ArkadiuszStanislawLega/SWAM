using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Models.ExternalSupplier;
using SWAM.Strings;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.ExternalSupplierPage
{
    /// <summary>
    /// Logika interakcji dla klasy CreateNewExternalSupplierTemplate.xaml
    /// </summary>
    public partial class CreateNewExternalSupplierTemplate : BasicUserControl
    {
        /// <summary>
        /// The name of the first phone number in the list with the supplier's numbers.
        /// </summary>
        private const string BASIC_PHONE_NAME = "właściciel";
        public CreateNewExternalSupplierTemplate()
        {
            InitializeComponent();
            this.ResidentalAddress.ShowEditControls();
        }

        #region AddNewExternalSupplier_Click
        /// <summary>
        /// Action after click add new external supplier button.
        /// Add new external supplier to database and update external supplier list view model.
        /// </summary>
        /// <param name="sender">Button add new external supplier.</param>
        /// <param name="e">Event click add new external supplier button.</param>
        private void AddNewExternalSupplier_Click(object sender, RoutedEventArgs e)
        {
            if (NameValidation(this.ExternalSupplierName.Text))
            {
                if (Models.EmailAddress.IsValidEmail(this.ExternalSupplierEmailAddress.Text))
                {
                    if (this.ResidentalAddress.GetAddress<ExternalSupplierAddress>() is ExternalSupplierAddress residentalAddress)
                    {
                        var external = new ExternalSupplier()
                        {
                            Name = this.ExternalSupplierName.Text,
                            Tin = this.ExternalSupplierTIN.Text,
                            Address = residentalAddress
                        };

                        ExternalSupplier.Add(external);

                        external.AddPhone(new ExternalSupplierPhone()
                        {
                            Note = BASIC_PHONE_NAME,
                            PhoneNumber = ExternalSupplierPhoneNumber.Text
                        });

                        external.EditEmail(new ExternalSupplierEmailAddress()
                        {
                            EmailAddress = this.ExternalSupplierEmailAddress.Text
                        });

                        InformationToUser($"Dodano nowego dostawcę: {this.ExternalSupplierName.Text}.");

                        ExternalSupplierListViewModel.Instance.Refresh();

                        ClearAllValues();
                    }
                    else
                        InformationToUser($"Błędny adres.", true);
                }
                else
                    InformationToUser($"Wprowadzony adres e-mail, jest nieprawidłowy.", true);
            }
        }
        #endregion
        #region  ClearAllValues
        /// <summary>
        /// Returns the editable fields to their initial state.
        /// </summary>
        private void ClearAllValues()
        {
            this.ExternalSupplierName.Text = string.Empty;
            this.ExternalSupplierTIN.Text = string.Empty;
            this.ExternalSupplierPhoneNumber.Text = string.Empty;
            this.ExternalSupplierEmailAddress.Text = string.Empty;

            this.ResidentalAddress.ClearEditValues();
        }
        #endregion
        #region NameValidation
        /// <summary>
        /// They will check the external supplier's name for length.
        /// </summary>
        /// <param name="name">Name to check.</param>
        /// <returns>True if the name meets the requirements.</returns>
        private bool NameValidation(string name)
        {
            //Check name - The name cannot be empty
            if (name != string.Empty)
            {
                char[] nameLength = name.ToCharArray();
                //the name must contain more than 3 letters
                if (nameLength.Length > SWAM.MainWindow.MIN_NAME_LENGTH)
                {
                    return true;
                }
                else InformationToUser($"Nazwa musi mieć więcej niż 3 litery.", true);
            }
            else InformationToUser($"Błędna nazwa.", true);

            return false;
        }
        #endregion
        #region TextChanged
        /// <summary>
        /// User can write only numbers.
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">New char in text field event</param>
        private void TextChanged(object sender, TextChangedEventArgs e)
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
