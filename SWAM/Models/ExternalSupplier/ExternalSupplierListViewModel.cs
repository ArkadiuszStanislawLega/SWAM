using System.Collections.ObjectModel;
using System.Linq;
using System.Data.Entity;
using System.Windows.Controls;

namespace SWAM.Models.ExternalSupplier
{
    public class ExternalSupplierListViewModel : UserControl
    {
        #region Properties
        /// <summary>
        /// External suppliers list view model, holds all external suppliers from database.
        /// </summary>
        private readonly ObservableCollection<ExternalSupplier> _externalSuppliers = new ObservableCollection<ExternalSupplier>();
        /// <summary>
        /// External suppliers list view model, holds all External suppliers from database.
        /// </summary>
        public ObservableCollection<ExternalSupplier> ExternalSuppliers => _externalSuppliers;
        #endregion

        #region SingletonePattern
        /// <summary>
        /// Constructor.
        /// </summary>
        static ExternalSupplierListViewModel() => _instance.Refresh();

        private static readonly ExternalSupplierListViewModel _instance = new ExternalSupplierListViewModel();
        public static ExternalSupplierListViewModel Instance => _instance;
        #endregion

        #region RefreshList
        /// <summary>
        /// Refreshes the list with data from the database.
        /// </summary>
        public void Refresh()
        {
            if(_externalSuppliers.Count > 0)
                    _externalSuppliers.Clear();

            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var externalSuppliers = context.People.OfType<ExternalSupplier>()
                    .Include(e => e.Phones)
                    .Include(e => e.Address)
                    .ToList();

                if (externalSuppliers != null && _externalSuppliers.Count > 0)
                    _externalSuppliers.Clear();

                foreach (var customer in externalSuppliers)
                {
                    _externalSuppliers.Add(customer);
                }
            }
        }
        #endregion
    }
}
