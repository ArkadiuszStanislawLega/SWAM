namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWarehouseAddressId : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Addresses", newName: "ints");
            DropForeignKey("dbo.Warehouses", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Warehouses", new[] { "Address_Id" });
            AddColumn("dbo.Warehouses", "AddressId", c => c.Int(nullable: false));
            DropColumn("dbo.Warehouses", "Address_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Warehouses", "Address_Id", c => c.Int());
            DropColumn("dbo.Warehouses", "AddressId");
            CreateIndex("dbo.Warehouses", "Address_Id");
            AddForeignKey("dbo.Warehouses", "Address_Id", "dbo.Addresses", "Id");
            RenameTable(name: "dbo.ints", newName: "Addresses");
        }
    }
}
