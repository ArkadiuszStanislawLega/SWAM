namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WarehousesModelUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Warehouses", "Height", c => c.Long(nullable: false));
            AddColumn("dbo.Warehouses", "Width", c => c.Long(nullable: false));
            AddColumn("dbo.Warehouses", "SurfaceAreaNetto", c => c.Long(nullable: false));
            AddColumn("dbo.Warehouses", "SurfaceAreaBrutton", c => c.Long(nullable: false));
            AddColumn("dbo.Warehouses", "AcceptableWeight", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Warehouses", "AcceptableWeight");
            DropColumn("dbo.Warehouses", "SurfaceAreaBrutton");
            DropColumn("dbo.Warehouses", "SurfaceAreaNetto");
            DropColumn("dbo.Warehouses", "Width");
            DropColumn("dbo.Warehouses", "Height");
        }
    }
}
