namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWarehouseModelCompositeKey : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Warehouses", "Id", "dbo.WarehouseAddress");
            DropIndex("dbo.Warehouses", new[] { "Id" });
            AddColumn("dbo.Warehouses", "WarehouseAddressId", c => c.Int(nullable: false));
            AddColumn("dbo.WarehouseAddress", "Warehouse_Id", c => c.Int());
            CreateIndex("dbo.WarehouseAddress", "Warehouse_Id");
            AddForeignKey("dbo.WarehouseAddress", "Warehouse_Id", "dbo.Warehouses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WarehouseAddress", "Warehouse_Id", "dbo.Warehouses");
            DropIndex("dbo.WarehouseAddress", new[] { "Warehouse_Id" });
            DropColumn("dbo.WarehouseAddress", "Warehouse_Id");
            DropColumn("dbo.Warehouses", "WarehouseAddressId");
            CreateIndex("dbo.Warehouses", "Id");
            AddForeignKey("dbo.Warehouses", "Id", "dbo.WarehouseAddress", "Id");
        }
    }
}
