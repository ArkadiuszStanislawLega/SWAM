﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        FindUserMessagesWindow
    }
}