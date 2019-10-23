﻿using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Models.ExternalSupplier;
using SWAM.Strings;

namespace SWAM.Controls.Templates.ExternalSupplierPage
{
    /// <summary>
    /// Logika interakcji dla klasy ExternalSupplierProfileTemplate.xaml
    /// </summary>
    public partial class ExternalSupplierProfileTemplate : BasicUserControl
    {
        public ExternalSupplierProfileTemplate()
        {
            InitializeComponent();

            this.ProperName.ConfirmChangeProperName.Click += ConfirmChangeProperName_Click;
            this.Tin.ConfirmChangeTIN.Click += ConfirmChangeTIN_Click;
        }

        #region ConfirmChangeProperName_Click
        /// <summary>
        /// Action after click confirm change proper name button.
        /// Update proper name of external supplier in database.
        /// </summary>
        /// <param name="sender">Confirm change proper name button.</param>
        /// <param name="e">Event click change proper name button.</param>
        private void ConfirmChangeProperName_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is ExternalSupplier externalSupplier)
            {
                externalSupplier.Edit(new ExternalSupplier()
                {
                    Id = externalSupplier.Id,
                    Name = this.ProperName.EditProperName.Text,
                    Tin = externalSupplier.Tin
                });
                DataContext = ExternalSupplier.Get(externalSupplier.Id);
                ExternalSupplierListViewModel.Instance.Refresh();

                InformationToUser($"Edytowano nazwę własną zewnętrzengo dostawcy {this.ProperName.EditProperName.Text}.");
            }
            else
                InformationToUser($"{ErrorMesages.EDIT_EXTERNAL_SUPPLIER_PROPER_NAME_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion
        #region ConfirmChangeTIN_Click
        /// <summary>
        /// Action after click confirm change TIN button.
        /// Update TIN of external supplier in database.
        /// </summary>
        /// <param name="sender">Confirm change TIN button.</param>
        /// <param name="e">Event click change TIN button.</param>
        private void ConfirmChangeTIN_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is ExternalSupplier externalSupplier)
            {
                externalSupplier.Edit(new ExternalSupplier()
                {
                    Id = externalSupplier.Id,
                    Name = externalSupplier.Name,
                    Tin = this.Tin.EditTin.Text
                });
                DataContext = ExternalSupplier.Get(externalSupplier.Id);

                this.Tin.EditTin.Text = string.Empty;
                InformationToUser($"Edytowano TIN zewnętrzego dostawcy {externalSupplier.Name}.");
            }
            else
                InformationToUser($"{ErrorMesages.EDIT_EXTERNAL_SUPPLIER_TIN_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion
        #region ConfirmEditResidentalAddress_Click
        /// <summary>
        /// Action after click confirm change residenta address button.
        /// Update residental address of external supplier in database.
        /// </summary>
        /// <param name="sender">Confirm change residental address button.</param>
        /// <param name="e">Event click change residental address button.</param>
        private void ConfirmEditResidentalAddress_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is ExternalSupplier externalSupplier)
            {
                if (this.ResidentalAddress.GetAddress<ExternalSupplierAddress>() is ExternalSupplierAddress editedAddress)
                {
                    externalSupplier.Address.Edit(editedAddress);
                    DataContext = ExternalSupplier.Get(externalSupplier.Id);
                    InformationToUser($"Edytowano adres zewnętrznego dostawcy.");
                }
                else InformationToUser($"{ErrorMesages.EDIT_EXTERNAL_SUPPLIER_ADDRESS_ERROR} {ErrorMesages.DATABASE_ERROR}", true);
            }
            else InformationToUser($"{ErrorMesages.EDIT_EXTERNAL_SUPPLIER_ADDRESS_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);

            this.ResidentalAddress.HideEditControls();
        }
        #endregion
        #region CancelEditResidentalAddress_Click
        /// <summary>
        /// Action after click cancel edit residental address button.
        /// Clear values hide edit controls.
        /// </summary>
        /// <param name="sender">Cancel change residental address button.</param>
        /// <param name="e">Event click cancel change residental address button.</param>
        private void CancelEditResidentalAddress_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ResidentalAddress.HideEditControls();
            if (DataContext is ExternalSupplier externalSupplier)
            {
                this.ResidentalAddress.DataContext = ExternalSupplier.Get(externalSupplier.Id).Address;
            }
            else InformationToUser($"{ErrorMesages.CANCEL_EDIT_EXTERNAL_SUPPLIER_ADDRESS_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion
        #region ChangeResidentalAddress_Click
        /// <summary>
        /// Action after click change residental address button.
        /// Show edit residental address controls.
        /// </summary>
        /// <param name="sender">Button change residental address.</param>
        /// <param name="e">Event click button change residental address.</param>
        private void ChangeResidentalAddress_Click(object sender, System.Windows.RoutedEventArgs e) 
            => this.ResidentalAddress.ShowEditControls();
        #endregion  

        private void SortAscending_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ConfirmSortButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }
    }
}
