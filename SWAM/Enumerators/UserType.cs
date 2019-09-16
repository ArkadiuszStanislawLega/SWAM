using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Enumerators
{
    /// <summary>
    /// System actors
    /// Permissions Are granted in MainWindow in property _pagesForUser.
    /// </summary>
    public enum UserType
    {
        /// <summary>
        /// This is for a person who has the authority to manage the user database and warehouses.
        /// Permissions to pages:
        /// - Loginpage,
        /// - AdministratorPage.
        /// </summary>
        Administrator,
        /// <summary>
        /// This is for the person who accepts the products to the warehouse and I issue them for couriers.
        /// Permissions to pages:
        /// - Loginpage,
        /// - ManageItemsPage,
        /// - ManageMagazinePage,
        /// - ManageOrdersPage.
        /// </summary>
        Warehouseman,
        /// <summary>
        /// Its for person who can sell our products do the clients.
        /// Permissions:
        /// - LoginPage,
        /// - ManageMagazinePage.
        /// </summary>
        Seller,
        /// <summary>
        /// Option for debug whole pages.
        /// Have permission to whole pages in appliaction.
        /// </summary>
        Programmer
    }
}