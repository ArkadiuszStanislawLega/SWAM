using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Models.ExternalSupplier;
using SWAM.Strings;
using System.Windows;

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

                ExternalSupplierListViewModel.Instance.Refresh();

                ClearAllValues();
            }
            else
                InformationToUser($"Błędny adres.", true);
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
    }
}
