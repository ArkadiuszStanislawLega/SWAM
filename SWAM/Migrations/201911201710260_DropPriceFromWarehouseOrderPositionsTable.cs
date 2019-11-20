namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropPriceFromWarehouseOrderPositionsTable : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.WarehouseOrderPositions", "Price");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WarehouseOrderPositions", "Price", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
    }
}
