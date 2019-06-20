﻿namespace SWAM.Enumerators
{
    /// <summary>
    /// An enumerator that stores all kinds of pages in the application.
    /// </summary>
    public enum PagesUserControls
    {
        EmptyPage,
        /// <summary>
        /// Page where can eneter only administrator of database.
        /// Administrator can there manage of the users rights.
        /// </summary>
        AdministratorPage,
        /// <summary>
        /// Page where all users must login in to use this application.
        /// </summary>
        LoginPage,
        /// <summary>
        /// Page where user can add/edit/delete all items in database.
        /// </summary>
        ManageItemsPage,
        /// <summary>
        /// Page where administrator can add/edit/delete warehouses.
        /// </summary>
        ManageMagazinePage,
        /// <summary>
        /// The site where the shop assistant and department manager can manage all orders.
        /// </summary>
        ManageOrdersPage,
    }
}
