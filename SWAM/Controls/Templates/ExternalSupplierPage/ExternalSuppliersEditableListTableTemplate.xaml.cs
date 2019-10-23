﻿using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Exceptions;
using SWAM.Models.ExternalSupplier;
using System.Windows;

namespace SWAM.Controls.Templates.ExternalSupplierPage
{
    /// <summary>
    /// Logika interakcji dla klasy ExternalSuppliersEditableListTableTemplate.xaml
    /// </summary>
    public partial class ExternalSuppliersEditableListTableTemplate : BasicUserControl
    {
        public ExternalSuppliersEditableListTableTemplate()
        {
            InitializeComponent();
        }
        #region ConfirmNewPhone_Click
        /// <summary>
        /// Action after click confirm new phone button.
        /// Adding new external supplier phone number to database, clear values in controls and refresh phones list.
        /// </summary>
        /// <param name="sender">Confirm new phone button.</param>
        /// <param name="e">Event click confirm new phone button.</param>
        private void ConfirmNewPhone_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ExternalSupplier externalSupplier)
            {
                externalSupplier.AddPhone(new ExternalSupplierPhone()
                {
                    Note = this.NewPhoneNote.Text,
                    PhoneNumber = this.NewPhone.Text,
                    ExternalSupplier = externalSupplier
                });
            }

            RefreshPhoneList();
            this.NewPhone.Text = string.Empty;
            this.NewPhone.Text = string.Empty;
        }
        #endregion
        #region CancelCreateNewPhone_Click
        /// <summary>
        /// Action after cancel create new phone button.
        /// Clear controls values.
        /// </summary>
        /// <param name="sender">Button cancel add new phone.</param>
        /// <param name="e">Event click button cancel add new phone.</param>
        private void CancelCreateNewPhone_Click(object sender, RoutedEventArgs e)
        {
            this.NewPhone.Text = string.Empty;
            this.NewPhone.Text = string.Empty;
        }
        #endregion
        #region RefreshPhoneList
        /// <summary>
        /// Refreshing view list of phones.
        /// </summary>
        public void RefreshPhoneList()
        {
            try
            {
                if (DataContext is ExternalSupplier externalSupplier)
                {
                    var phonesList = externalSupplier.GetExternalSupplierPhones();
                    if (phonesList != null)
                        PhoneNumbers.ItemsSource = phonesList;
                    else throw new RefreshExternalsupplierPhonesListException();
                }
                else throw new RefreshExternalsupplierPhonesListException();
            }
            catch (RefreshExternalsupplierPhonesListException ex) { ex.ShowMessage(this); }
        }
        #endregion
    }
}
