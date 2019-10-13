namespace SWAM.Enumerators
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
        /// <summary>
        /// The page where the logged in user in the application can check his account settings.
        /// </summary>
        LogedInUserProfile,
        /// <summary>
        /// The page where user can check his emails.
        /// </summary>
        MessagesPage,
        /// <summary>
        /// A page where you can manage your customer database.
        /// </summary>
        ManageCustomersPage,
        /// <summary>
        /// A page where you can manage your couriers database.
        /// </summary>
        ManageCouriersPage,
        /// <summary>
        /// A page where you can manage your external suppliers database.
        /// </summary>
        ManageExternalSuppliersPage
    }
}
