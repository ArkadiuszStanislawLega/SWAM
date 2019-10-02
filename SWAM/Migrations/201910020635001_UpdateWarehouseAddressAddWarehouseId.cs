namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWarehouseAddressAddWarehouseId : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WarehouseAddress", "WarehouseId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WarehouseAddress", "WarehouseId");
        }
    }
}
