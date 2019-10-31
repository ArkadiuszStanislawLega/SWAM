using SWAM.Models.Customer;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace SWAM.Models.ManageOrdersPage
{
    public class CustomerOrdersListViewModel : UserControl
    {
        private readonly ObservableCollection<CustomerOrder> _customerOrders = new ObservableCollection<CustomerOrder>();
        public ObservableCollection<CustomerOrder> CustomerOrders => this._customerOrders;

        #region SingletonePattern
        /// <summary>
        /// Constructor.
        /// </summary>
        static CustomerOrdersListViewModel() { _instance.Refresh(); }

        private static readonly CustomerOrdersListViewModel _instance = new CustomerOrdersListViewModel();
        public static CustomerOrdersListViewModel Instance => _instance;
        public CustomerOrdersListViewModel() { }
        #endregion

        public void Refresh()
        {
            if (CustomerOrder.GetAllOrders() is IList<CustomerOrder> list)
            {
                if (this._customerOrders.Count > 0)
                    this._customerOrders.Clear();

                foreach (var item in list)
                {
                    this._customerOrders.Add(item);
                }
            }
        }
    }
}
