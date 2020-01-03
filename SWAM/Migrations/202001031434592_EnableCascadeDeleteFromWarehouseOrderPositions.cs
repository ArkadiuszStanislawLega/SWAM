namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnableCascadeDeleteFromWarehouseOrderPositions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WarehouseOrderPositions", "WarehouseOrder_Id", "dbo.WarehouseOrders");
            DropIndex("dbo.WarehouseOrderPositions", new[] { "WarehouseOrder_Id" });
            AlterColumn("dbo.WarehouseOrderPositions", "WarehouseOrder_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.WarehouseOrderPositions", "WarehouseOrder_Id");
            AddForeignKey("dbo.WarehouseOrderPositions", "WarehouseOrder_Id", "dbo.WarehouseOrders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WarehouseOrderPositions", "WarehouseOrder_Id", "dbo.WarehouseOrders");
            DropIndex("dbo.WarehouseOrderPositions", new[] { "WarehouseOrder_Id" });
            AlterColumn("dbo.WarehouseOrderPositions", "WarehouseOrder_Id", c => c.Int());
            CreateIndex("dbo.WarehouseOrderPositions", "WarehouseOrder_Id");
            AddForeignKey("dbo.WarehouseOrderPositions", "WarehouseOrder_Id", "dbo.WarehouseOrders", "Id");
        }
    }
}
