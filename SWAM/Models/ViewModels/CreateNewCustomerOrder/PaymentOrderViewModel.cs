using SWAM.Models.ProductOrderList;
using System;
using System.ComponentModel;

namespace SWAM.Models.ViewModels.CreateNewCustomerOrder
{
    public class PaymentOrderViewModel : INotifyPropertyChanged
    {
        private decimal _totalPrice;
        public decimal TotalPrice { get { return _totalPrice; } private set { _totalPrice = value; NotifyPropertyChanged("TotalPrice"); }  }

        #region Singletone Pattern
        static PaymentOrderViewModel() => _instance.Refresh();

        private static readonly PaymentOrderViewModel _instance = new PaymentOrderViewModel();
        public static PaymentOrderViewModel Instance => _instance;
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(String propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #region Refresh
        public void Refresh()
        {
            TotalPrice = 0;
            foreach (var item in ProductOrderListViewModel.Instance.CustomerOrderPositions)
            {
                TotalPrice += item.Price * item.Quantity;
            }
        }
        #endregion
    }
}
