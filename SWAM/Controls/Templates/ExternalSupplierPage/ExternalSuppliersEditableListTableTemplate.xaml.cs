using SWAM.Controls.Templates.AdministratorPage;
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

        private void ConfirmNewPhone_Click(object sender, RoutedEventArgs e)
        {
            if (DataContext is ExternalSupplier externalSupplier)
            {
                externalSupplier.AddPhone(new ExternalSupplierPhone()
                {
                    Note = NewPhoneNote.Text,
                    PhoneNumber = NewPhone.Text,
                    ExternalSupplier = externalSupplier
                });
            }

            RefreshPhoneList();
        }

        private void CancelCreateNewPhone_Click(object sender, RoutedEventArgs e)
        {

        }

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
