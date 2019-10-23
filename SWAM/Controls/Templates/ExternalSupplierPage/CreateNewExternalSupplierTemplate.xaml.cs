using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Models.ExternalSupplier;
using System.Windows;

namespace SWAM.Controls.Templates.ExternalSupplierPage
{
    /// <summary>
    /// Logika interakcji dla klasy CreateNewExternalSupplierTemplate.xaml
    /// </summary>
    public partial class CreateNewExternalSupplierTemplate : BasicUserControl
    {
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
                ExternalSupplierListViewModel.Instance.Refresh();

                this.ExternalSupplierName.Text = string.Empty;
                this.ExternalSupplierTIN.Text = string.Empty;
                this.ResidentalAddress.ClearEditValues();
            }
        }
        #endregion
    }
}
