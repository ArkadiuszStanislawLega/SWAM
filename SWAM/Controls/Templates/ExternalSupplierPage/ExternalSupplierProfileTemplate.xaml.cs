using SWAM.Controls.Templates.AdministratorPage;

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

        private void ConfirmChangeTIN_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void ConfirmChangeProperName_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        private void SortAscending_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ConfirmSortButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void TextBox_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {

        }

        private void CancelEditResidentalAddress_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.ResidentalAddress.HideEditControls();
            this.ResidentalAddress.ClearEditValues();
        }

        private void ConfirmEditResidentalAddress_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }

        private void ChangeResidentalAddress_Click(object sender, System.Windows.RoutedEventArgs e) => this.ResidentalAddress.ShowEditControls();
        
    }
}
