﻿using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Controls;

namespace SWAM.Models.Courier
{
    public class CouriersListViewModel : UserControl
    {
        #region Properties
        /// <summary>
        /// Customers list view model, holds all couriers from database.
        /// </summary>
        private static readonly ObservableCollection<Courier> _couriersList = new ObservableCollection<Courier>();
        /// <summary>
        /// Customers list view model, holds all couriers from database.
        /// </summary>
        public static ObservableCollection<Courier> CouriersList => _couriersList;
        #endregion

        #region SingletonePattern
        /// <summary>
        /// Constructor.
        /// </summary>
        static CouriersListViewModel() => _instance.Refresh();

        private static readonly CouriersListViewModel _instance = new CouriersListViewModel();
        public static CouriersListViewModel Instance => _instance;
        #endregion

        #region RefreshList
        /// <summary>
        /// Refreshes the list with data from the database.
        /// </summary>
        public void Refresh()
        {
            //TODO: Change this, remove to the couriers class.
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                var couriers = context.People.OfType<Courier>().ToList();

                if (couriers != null && _couriersList.Count > 0)
                    _couriersList.Clear();

                foreach (var courier in couriers)
                {
                    _couriersList.Add(courier);
                }
            }
        }
        #endregion
    }
}