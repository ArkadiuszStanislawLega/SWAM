﻿using SWAM.Controls.Templates.AdministratorPage;
using SWAM.Exceptions;
using SWAM.Models.ExternalSupplier;
using SWAM.Strings;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace SWAM.Controls.Templates.ExternalSupplierPage
{
    /// <summary>
    /// Logika interakcji dla klasy ExternalSuppliersEditablePhoneListTemplate.xaml
    /// </summary>
    public partial class ExternalSuppliersEditablePhoneListTemplate : BasicUserControl
    {
        public ExternalSuppliersEditablePhoneListTemplate()
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

                InformationToUser($"Został dodany nowy numer kontaktowy telefonu dostawcy: {this.NewPhoneNote.Text}- {this.NewPhone.Text}.");

                RefreshPhoneList();

                this.NewPhone.Text = string.Empty;
                this.NewPhoneNote.Text = string.Empty;
            }
            else
                InformationToUser($"{ErrorMesages.ADD_EXTERNAL_SUPPLIER_PHONE_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);


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
        #region NewPhone_TextChanged
        /// <summary>
        /// User can write only numbers.
        /// </summary>
        /// <param name="sender">TextBox</param>
        /// <param name="e">New char in text field event</param>
        private void NewPhone_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
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
