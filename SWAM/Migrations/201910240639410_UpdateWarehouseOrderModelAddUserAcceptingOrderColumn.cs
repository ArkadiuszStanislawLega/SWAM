namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWarehouseOrderModelAddUserAcceptingOrderColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WarehouseOrders", "UserAcceptingOrder_Id", c => c.Int());
            CreateIndex("dbo.WarehouseOrders", "UserAcceptingOrder_Id");
            AddForeignKey("dbo.WarehouseOrders", "UserAcceptingOrder_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WarehouseOrders", "UserAcceptingOrder_Id", "dbo.Users");
            DropIndex("dbo.WarehouseOrders", new[] { "UserAcceptingOrder_Id" });
            DropColumn("dbo.WarehouseOrders", "UserAcceptingOrder_Id");
        }
    }
}
