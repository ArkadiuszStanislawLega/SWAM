
namespace SWAM.Enumerators
{
    public enum BookmarkInPage
    {
        EmptyBookmark,
        /// <summary>
        /// Manage of warehoueses by administrator in <see cref="AdministratorPage"/>.
        /// </summary>
        WarehousesControlPanel,
        /// <summary>
        /// Manage of users by administrator in <see cref="AdministratorPage"/>.
        /// </summary>
        UsersControlPanel,
        /// <summary>
        /// Manage of warehouses orders.
        /// </summary>
        WarehouseOrdersPanel,
        /// <summary>
        /// Manage of customers orders.
        /// </summary>
        CustomerOrdersPanel,
        /// <summary>
        /// Sending message template in <see cref="SendMessageMessagesWindow"/>.
        /// </summary>
        SendMessageMessagesWindow,
        /// <summary>
        /// Finding user template in <see cref="SendMessageMessagesWindow"/>.
        /// </summary>
        FindUserMessagesWindow,
        /// <summary>
        /// A tab in <see cref="ManageOrdersPage"/> with orders for the warehouse.
        /// </summary>
        ToWarehousesOrdersList,
        /// <summary>
        /// A tab in <see cref="ManageCouriersPage"/> with courier profile.
        /// </summary>
        CourierProfile,
        /// <summary>
        /// A tab in <see cref="ManageCouriersPage"/> with create new courier panel.
        /// </summary>
        CreateNewCourier,
        /// <summary>
        /// A tab in <see cref="ManageCustomerPage"/> with customer profile.
        /// </summary>
        CustomerProfile,
        /// <summary>
        /// A tab in <see cref="ManageCustomerPage"/> with create new customer panel.
        /// </summary>
        CreateNewCustomer,
        /// <summary>
        /// A tab in <see cref="ManageExternalSupplierPage"/> with external supplier profile.
        /// </summary>
        ExternalSupplierProfile,
        /// <summary>
        /// A tab in <see cref="ManageExternalSupplierPage"/> with create new external supplier panel.
        /// </summary>
        CreateNewExternalSupplier,
    }
}
