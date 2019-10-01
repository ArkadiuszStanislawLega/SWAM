namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWarehouseAddressOptional : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.WarehouseAddress", "WarehouseId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WarehouseAddress", "WarehouseId", c => c.Int(nullable: false));
        }
    }
}
