using SWAM.Models.Warehouse;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Data.Entity;
using System.Linq;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System;
using SWAM.Strings;

namespace SWAM.Models.ExternalSupplier
{
    public class ExternalSupplierDeliveryListViewModel: UserControl
    {
        private readonly ObservableCollection<WarehouseOrder> _warehouseOrders = new ObservableCollection<WarehouseOrder>();

        public ObservableCollection<WarehouseOrder> WarehouseOrders => _warehouseOrders;

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
        /// <param name="externalSupplier">External supplier whose data we want to download from the database.</param>
        public void Refresh(ExternalSupplier externalSupplier)
        {
            if (externalSupplier != null)
            {
                if (_warehouseOrders.Count > 0)
                    _warehouseOrders.Clear();

                try
                {
                    using (ApplicationDbContext context = new ApplicationDbContext())
                    {
                        var warehouseDelivery = context
                            .WarehouseOrders
                            .Include(w => w.OrderPositions)
                            .Include(w => w.Warehouse)
                            .Include(w => w.Creator)
                            .Include(w => w.UserReceivedOrder)
                            .Where(w => w.ExternalSupplayer.Id == externalSupplier.Id)
                            .ToList();

                        foreach (var order in warehouseDelivery)
                        {
                            for (int i = 0; i < order.OrderPositions.Count; i++)
                            {
                                var id = order.OrderPositions[i].Id;
                                order.OrderPositions[i] = context.WarehouseOrderPositions
                                    .Include(w => w.Product)
                                    .FirstOrDefault(w => w.Id == id);
                            }

                            if (order.UserReceivedOrder != null)
                            {
                                var userId = order.UserReceivedOrder.Id;
                                order.UserReceivedOrder = context.People
                                    .OfType<User.User>()
                                    .Include(u => u.Phones)
                                    .FirstOrDefault(u => u.Id == userId);
                            }

                            if (order.Creator != null)
                            {
                                var userId = order.Creator.Id;
                                order.Creator = context.People
                                    .OfType<User.User>()
                                    .Include(u => u.Phones)
                                    .FirstOrDefault(u => u.Id == userId);
                            }

                            _warehouseOrders.Add(order);
                        }
                    }
                }
                catch (DbUpdateConcurrencyException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                }
                catch (DbUpdateException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                }
                catch (DbEntityValidationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                }
                catch (NotSupportedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                }
                catch (ObjectDisposedException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                }
                catch (InvalidOperationException e)
                {
                    MainWindow.Instance.WarningWindow.Show(e.Message, ErrorMesages.DATABASE_ERROR);
                }
            }
        }
        #endregion
    }
}
