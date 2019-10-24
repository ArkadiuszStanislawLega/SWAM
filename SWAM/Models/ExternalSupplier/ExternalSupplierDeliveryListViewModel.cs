using SWAM.Models.Warehouse;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Data.Entity;
using System.Linq;

namespace SWAM.Models.ExternalSupplier
{
    public class ExternalSupplierDeliveryListViewModel: UserControl
    {
        private static readonly ObservableCollection<WarehouseOrder> _warehouseOrders = new ObservableCollection<WarehouseOrder>();

        public static ObservableCollection<WarehouseOrder> WarehouseOrders => _warehouseOrders;

        #region SingletonePattern
        /// <summary>
        /// Constructor.
        /// </summary>
        static ExternalSupplierDeliveryListViewModel() { }

        private static readonly ExternalSupplierDeliveryListViewModel _instance = new ExternalSupplierDeliveryListViewModel();
        public static ExternalSupplierDeliveryListViewModel Instance => _instance;
        #endregion

        #region RefreshList
        /// <summary>
        /// Refreshes the list with data from the database.
        /// </summary>
        public void Refresh(ExternalSupplier externalSupplier)
        {
            if (_warehouseOrders.Count > 0)
                _warehouseOrders.Clear();

            //TODO: Remove this to model
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var warehouseDelivery = context
                    .WarehouseOrders
                    .Include(w => w.OrderPositions)
                    .Include(w => w.WarehouseOrderStatus)
                    .Include(w => w.UserAcceptingOrder)
                    .Where(w => w.ExternalSupplayer.Id == externalSupplier.Id)
                    .ToList();

                foreach (var order in warehouseDelivery)
                {
                    _warehouseOrders.Add(order);
                }
            }
        }
        #endregion
    }
}
