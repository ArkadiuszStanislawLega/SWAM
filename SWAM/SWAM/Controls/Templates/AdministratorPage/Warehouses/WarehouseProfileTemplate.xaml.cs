using SWAM.Models;
using SWAM.Models.AdministratorPage;
using SWAM.Templates.AdministratorPage;
using System.Windows;
using SWAM.Strings;


namespace SWAM.Controls.Templates.AdministratorPage.Warehouses
{
    /// <summary>
    /// Logika interakcji dla klasy WarehouseProfileTemplate.xaml
    /// </summary>
    public partial class WarehouseProfileTemplate : BasicUserControl
    {
        public WarehouseProfileTemplate()
        {
            InitializeComponent();
        }
        #region WarehouseProfileTemplate_Loaded
        private void WarehouseProfileTemplate_Loaded(object sender, RoutedEventArgs e)
        {
            WarehousesListViewModel warehouses = new WarehousesListViewModel();
            warehouses.Refresh();

            if (DataContext is Warehouse data)
            {
                foreach (Warehouse w in warehouses.WarehousesList)
                {
                    if (w.Id == data.Id)
                    {
                        DataContext = w;
                        break;
                    }
                }
            }
        }
        #endregion

        #region Edit_Click
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            this.Address.ShowEditControls();
            this.WarehouseTechnicalDate.ShowEditControls();
        }
        #endregion

        #region Delete_Click
        /// <summary>
        /// Action after click delete warehouse button.
        /// </summary>
        /// <param name="sender">Delete button</param>
        /// <param name="e">Action button clicked</param>
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            string correctDeleteWarehouseMessage;

            if (DataContext is Warehouse data)
            {
                this._confirmWindow.Show($"Czy na pewno chcesz usunąć magazyn {data.Name}?", out bool isConfirmed, "Potwierdź usunięcie magazynu");
                if(isConfirmed)
                {
                    if (data.Remove())
                    {
                        correctDeleteWarehouseMessage = $"Magazyn { data.Name} został usunięty.";
                        InformationToUser(correctDeleteWarehouseMessage);
                        //Refresh List with warehouses
                        if (SWAM.MainWindow.FindParent<WarehousesControlPanelTemplate>(this) is WarehousesControlPanelTemplate template)
                            template.RefreshList();
                        else InformationToUser($"{correctDeleteWarehouseMessage} {ErrorMesages.DURGIN_DELETE_WAREHOUSE_ERROR} {ErrorMesages.REFRESH_WAREHOUSES_LIST_ERROR}", true);
                    }
                    else InformationToUser($"{ErrorMesages.DURGIN_DELETE_WAREHOUSE_ERROR}", true);
                }
            }
            else InformationToUser($"{ErrorMesages.DURGIN_DELETE_WAREHOUSE_ERROR} {ErrorMesages.DATACONTEXT_ERROR}", true);
        }
        #endregion

        #region Confirm_Click
        private void Confirm_Click(object sender, RoutedEventArgs e)
        {
            this.Address.HideEditControls();
            this.WarehouseTechnicalDate.HideEditControls();
        }
        #endregion
    }
}
