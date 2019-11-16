namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropWarehouseAdressIdFromWarehousesTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Warehouses", "WarehouseAddressId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Warehouses", "WarehouseAddressId", c => c.Int(nullable: false));
        }
    }
}
