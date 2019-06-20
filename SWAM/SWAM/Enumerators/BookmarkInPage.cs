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
        /// Manage of warehoueses by administrator in Administator page.
        /// </summary>
        WarehousesControlPanel,
        /// <summary>
        /// Manage of users by administrator in Administator page.
        /// </summary>
        UsersControlPanel,
        //TODO: Make documentation
        WarehouseOrdersPanel,
        CustomerOrdersPanel
    }
}
