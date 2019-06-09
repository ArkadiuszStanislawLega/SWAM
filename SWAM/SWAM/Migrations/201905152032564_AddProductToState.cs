namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddProductToState : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.States", "Product_Id", c => c.Int());
            CreateIndex("dbo.States", "Product_Id");
            AddForeignKey("dbo.States", "Product_Id", "dbo.Products", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.States", "Product_Id", "dbo.Products");
            DropIndex("dbo.States", new[] { "Product_Id" });
            DropColumn("dbo.States", "Product_Id");
        }
    }
}
