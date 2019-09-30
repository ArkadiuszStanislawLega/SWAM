namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOneToManyRelationshipBetweenProductsAndStates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.States", "Warehouse_Id", "dbo.Warehouses");
            DropIndex("dbo.States", new[] { "Warehouse_Id" });
            RenameColumn(table: "dbo.States", name: "Warehouse_Id", newName: "WarehouseId");
            AddColumn("dbo.States", "ProductId", c => c.Int(nullable: false));
            AlterColumn("dbo.States", "WarehouseId", c => c.Int(nullable: false));
            CreateIndex("dbo.States", "ProductId");
            CreateIndex("dbo.States", "WarehouseId");
            AddForeignKey("dbo.States", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
            AddForeignKey("dbo.States", "WarehouseId", "dbo.Warehouses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.States", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.States", "ProductId", "dbo.Products");
            DropIndex("dbo.States", new[] { "WarehouseId" });
            DropIndex("dbo.States", new[] { "ProductId" });
            AlterColumn("dbo.States", "WarehouseId", c => c.Int());
            DropColumn("dbo.States", "ProductId");
            RenameColumn(table: "dbo.States", name: "WarehouseId", newName: "Warehouse_Id");
            CreateIndex("dbo.States", "Warehouse_Id");
            AddForeignKey("dbo.States", "Warehouse_Id", "dbo.Warehouses", "Id");
        }
    }
}
