namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWarehousesChangeContatctsToUsers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Phones", "Warehouse_Id", "dbo.Warehouses");
            DropIndex("dbo.Phones", new[] { "Warehouse_Id" });
            AddColumn("dbo.Users", "Warehouse_Id", c => c.Int());
            CreateIndex("dbo.Users", "Warehouse_Id");
            AddForeignKey("dbo.Users", "Warehouse_Id", "dbo.Warehouses", "Id");
            DropColumn("dbo.Phones", "Warehouse_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Phones", "Warehouse_Id", c => c.Int());
            DropForeignKey("dbo.Users", "Warehouse_Id", "dbo.Warehouses");
            DropIndex("dbo.Users", new[] { "Warehouse_Id" });
            DropColumn("dbo.Users", "Warehouse_Id");
            CreateIndex("dbo.Phones", "Warehouse_Id");
            AddForeignKey("dbo.Phones", "Warehouse_Id", "dbo.Warehouses", "Id");
        }
    }
}
