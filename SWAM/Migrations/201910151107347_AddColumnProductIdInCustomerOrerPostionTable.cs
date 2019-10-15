namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddColumnProductIdInCustomerOrerPostionTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerOrderPositions", "Product_Id", "dbo.Products");
            DropIndex("dbo.CustomerOrderPositions", new[] { "Product_Id" });
            RenameColumn(table: "dbo.CustomerOrderPositions", name: "Product_Id", newName: "ProductId");
            AlterColumn("dbo.CustomerOrderPositions", "ProductId", c => c.Int(nullable: false));
            CreateIndex("dbo.CustomerOrderPositions", "ProductId");
            AddForeignKey("dbo.CustomerOrderPositions", "ProductId", "dbo.Products", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerOrderPositions", "ProductId", "dbo.Products");
            DropIndex("dbo.CustomerOrderPositions", new[] { "ProductId" });
            AlterColumn("dbo.CustomerOrderPositions", "ProductId", c => c.Int());
            RenameColumn(table: "dbo.CustomerOrderPositions", name: "ProductId", newName: "Product_Id");
            CreateIndex("dbo.CustomerOrderPositions", "Product_Id");
            AddForeignKey("dbo.CustomerOrderPositions", "Product_Id", "dbo.Products", "Id");
        }
    }
}
