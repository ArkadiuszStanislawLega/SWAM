using SWAM.Models.Warehouse;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWAM.Controls.Templates.ManageOrdersPage.ManageWarehouseOrdersPage.NewOrder.Warehouses.Validators
{
    public class CreateNewWarehouseOrderValidator
    {
        public bool WarehouseValidator(Warehouse warehouse)
        {
            if (warehouse == null)
            {
                return false;
            }
            return true;
        }
    }
}
