namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductIdForeignKeyPropertyToWarehouseOrderPositionTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WarehouseOrderPositions", "Product_Id", "dbo.Products");
            DropIndex("dbo.WarehouseOrderPositions", new[] { "Product_Id" });
            RenameColumn(table: "dbo.WarehouseOrderPositions", name: "Product_Id", newName: "ProductId");
            AlterColumn("dbo.WarehouseOrderPositions", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.WarehouseOrderPositions", "ProductId");
            AddForeignKey("dbo.WarehouseOrderPositions", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WarehouseOrderPositions", "ProductId", "dbo.Products");
            DropIndex("dbo.WarehouseOrderPositions", new[] { "ProductId" });
            AlterColumn("dbo.WarehouseOrderPositions", "ProductId", c => c.Int());
            RenameColumn(table: "dbo.WarehouseOrderPositions", name: "ProductId", newName: "Product_Id");
            CreateIndex("dbo.WarehouseOrderPositions", "Product_Id");
            AddForeignKey("dbo.WarehouseOrderPositions", "Product_Id", "dbo.Products", "Id");
        }
    }
}
