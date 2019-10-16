namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddForeignKeyPropertiesToCustomerOrdersTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CustomerOrders", name: "Warehouse_Id", newName: "WarehouseId");
            RenameColumn(table: "dbo.CustomerOrders", name: "Courier_Id", newName: "CourierId");
            RenameColumn(table: "dbo.CustomerOrders", name: "Customer_Id", newName: "CustomerId");
            RenameColumn(table: "dbo.CustomerOrders", name: "User_Id", newName: "UserId");
            RenameIndex(table: "dbo.CustomerOrders", name: "IX_Warehouse_Id", newName: "IX_WarehouseId");
            RenameIndex(table: "dbo.CustomerOrders", name: "IX_User_Id", newName: "IX_UserId");
            RenameIndex(table: "dbo.CustomerOrders", name: "IX_Customer_Id", newName: "IX_CustomerId");
            RenameIndex(table: "dbo.CustomerOrders", name: "IX_Courier_Id", newName: "IX_CourierId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CustomerOrders", name: "IX_CourierId", newName: "IX_Courier_Id");
            RenameIndex(table: "dbo.CustomerOrders", name: "IX_CustomerId", newName: "IX_Customer_Id");
            RenameIndex(table: "dbo.CustomerOrders", name: "IX_UserId", newName: "IX_User_Id");
            RenameIndex(table: "dbo.CustomerOrders", name: "IX_WarehouseId", newName: "IX_Warehouse_Id");
            RenameColumn(table: "dbo.CustomerOrders", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.CustomerOrders", name: "CustomerId", newName: "Customer_Id");
            RenameColumn(table: "dbo.CustomerOrders", name: "CourierId", newName: "Courier_Id");
            RenameColumn(table: "dbo.CustomerOrders", name: "WarehouseId", newName: "Warehouse_Id");
        }
    }
}
