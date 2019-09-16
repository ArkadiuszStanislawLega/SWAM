namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WarehouseModelReview : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Warehouses", "_addressId", c => c.Int(nullable: false));
            CreateIndex("dbo.Warehouses", "_addressId");
            AddForeignKey("dbo.Warehouses", "_addressId", "dbo.Addresses", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Warehouses", "_addressId", "dbo.Addresses");
            DropIndex("dbo.Warehouses", new[] { "_addressId" });
            DropColumn("dbo.Warehouses", "_addressId");
        }
    }
}
