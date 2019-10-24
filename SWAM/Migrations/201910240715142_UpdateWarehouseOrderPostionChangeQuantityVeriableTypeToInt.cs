namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateWarehouseOrderPostionChangeQuantityVeriableTypeToInt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.WarehouseOrderPositions", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.WarehouseOrderPositions", "Quantity");
        }
    }
}
