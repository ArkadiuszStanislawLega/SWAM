using SWAM.Models.Warehouse;
using System.Collections.Generic;

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

        public bool OrderedProductsValidation(List<WarehouseOrderPosition> warehouseOrderPositions)
        {
            if (warehouseOrderPositions.Count == 0)
            {
                return false;
            }
            return true;
        }
    }
}
