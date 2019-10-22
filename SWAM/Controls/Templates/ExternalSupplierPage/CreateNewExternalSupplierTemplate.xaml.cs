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
        }

        private void AddNewExternalSupplier_Click(object sender, RoutedEventArgs e)
        {
            var external = new ExternalSupplier()
            {
                Name = this.ExternalSupplierName.Text
            };

            ExternalSupplier.Add(external);

            ExternalSupplierListViewModel.Instance.Refresh();
        }
    }
}
