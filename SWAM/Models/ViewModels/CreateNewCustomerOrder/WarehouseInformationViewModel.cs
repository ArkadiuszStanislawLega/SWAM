using System;
using System.ComponentModel;

namespace SWAM.Models.ViewModels.CreateNewCustomerOrder
{
    public class WarehouseInformationViewModel : INotifyPropertyChanged
    {
        private Warehouse.Warehouse _warehouse;
        public Warehouse.Warehouse Warehouse { get { return _warehouse; } private set { _warehouse = value; NotifyPropertyChanged("Warehouse"); } }

        #region Singletone Pattern
        private static readonly WarehouseInformationViewModel _instance = new WarehouseInformationViewModel();
        public static WarehouseInformationViewModel Instance => _instance;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region SetWarehouse
        public void SetWarehouse( Warehouse.Warehouse warehouse)
        {
            Warehouse = warehouse;
        }
        #endregion
    }
}
