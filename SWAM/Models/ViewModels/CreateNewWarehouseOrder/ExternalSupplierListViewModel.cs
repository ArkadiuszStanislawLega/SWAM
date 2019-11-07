using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;

namespace SWAM.Models.ViewModels.CreateNewWarehouseOrder
{
    public class ExternalSupplierListViewModel
    {
        #region Properties
        ApplicationDbContext context = new ApplicationDbContext();
        private readonly ObservableCollection<ExternalSupplier.ExternalSupplier> _externalSuppliers = new ObservableCollection<ExternalSupplier.ExternalSupplier>();
        public ObservableCollection<ExternalSupplier.ExternalSupplier> ExternalSuppliers => _externalSuppliers;
        #endregion

        #region SingletonePattern
        /// <summary>
        /// Constructor.
        /// </summary>
        static ExternalSupplierListViewModel() => _instance.Refresh();

        private static readonly ExternalSupplierListViewModel _instance = new ExternalSupplierListViewModel();
        public static ExternalSupplierListViewModel Instance => _instance;
        #endregion

        #region Refresh
        /// <summary>
        /// Refreshes the list with data from the database.
        /// </summary>
        public void Refresh()
        {
            if (_externalSuppliers != null && _externalSuppliers.Count > 0)
                _externalSuppliers.Clear();

            var externalSuppliers = context.ExternalSuppliers
                .Include(e => e.Address)
                .Include(e => e.Phones)
                .ToList();

            foreach (var externalSupplier in externalSuppliers)
            {
                _externalSuppliers.Add(externalSupplier);
            }
        }
        #endregion
    }
}
