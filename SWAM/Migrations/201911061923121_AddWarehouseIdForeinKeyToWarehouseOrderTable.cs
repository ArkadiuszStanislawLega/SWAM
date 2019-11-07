namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddWarehouseIdForeinKeyToWarehouseOrderTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.WarehouseOrders", name: "Warehouse_Id", newName: "WarehouseId");
            RenameIndex(table: "dbo.WarehouseOrders", name: "IX_Warehouse_Id", newName: "IX_WarehouseId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.WarehouseOrders", name: "IX_WarehouseId", newName: "IX_Warehouse_Id");
            RenameColumn(table: "dbo.WarehouseOrders", name: "WarehouseId", newName: "Warehouse_Id");
        }
    }
}
