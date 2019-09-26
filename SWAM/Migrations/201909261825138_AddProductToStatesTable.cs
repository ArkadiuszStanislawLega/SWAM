namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductToStatesTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.States", name: "Warehouse_Id", newName: "WarehouseId");
            RenameIndex(table: "dbo.States", name: "IX_Warehouse_Id", newName: "IX_WarehouseId");
            AddColumn("dbo.States", "Product_Id", c => c.Int());
            CreateIndex("dbo.States", "Product_Id");
            AddForeignKey("dbo.States", "Product_Id", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.States", "Product_Id", "dbo.Products");
            DropIndex("dbo.States", new[] { "Product_Id" });
            DropColumn("dbo.States", "Product_Id");
            RenameIndex(table: "dbo.States", name: "IX_WarehouseId", newName: "IX_Warehouse_Id");
            RenameColumn(table: "dbo.States", name: "WarehouseId", newName: "Warehouse_Id");
        }
    }
}
