namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFieldsToWarehouseOrdersTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.WarehouseOrders", name: "UserAcceptingOrder_Id", newName: "UserReceivedOrderId");
            RenameColumn(table: "dbo.WarehouseOrders", name: "ExternalSupplayer_Id", newName: "ExternalSupplayerId");
            RenameIndex(table: "dbo.WarehouseOrders", name: "IX_UserAcceptingOrder_Id", newName: "IX_UserReceivedOrderId");
            RenameIndex(table: "dbo.WarehouseOrders", name: "IX_ExternalSupplayer_Id", newName: "IX_ExternalSupplayerId");
            AddColumn("dbo.WarehouseOrders", "IsPaid", c => c.Boolean(nullable: false));
            AddColumn("dbo.WarehouseOrders", "CreatorId", c => c.Int(nullable: false));
            CreateIndex("dbo.WarehouseOrders", "CreatorId");
            AddForeignKey("dbo.WarehouseOrders", "CreatorId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WarehouseOrders", "CreatorId", "dbo.Users");
            DropIndex("dbo.WarehouseOrders", new[] { "CreatorId" });
            DropColumn("dbo.WarehouseOrders", "CreatorId");
            DropColumn("dbo.WarehouseOrders", "IsPaid");
            RenameIndex(table: "dbo.WarehouseOrders", name: "IX_ExternalSupplayerId", newName: "IX_ExternalSupplayer_Id");
            RenameIndex(table: "dbo.WarehouseOrders", name: "IX_UserReceivedOrderId", newName: "IX_UserAcceptingOrder_Id");
            RenameColumn(table: "dbo.WarehouseOrders", name: "ExternalSupplayerId", newName: "ExternalSupplayer_Id");
            RenameColumn(table: "dbo.WarehouseOrders", name: "UserReceivedOrderId", newName: "UserAcceptingOrder_Id");
        }
    }
}
