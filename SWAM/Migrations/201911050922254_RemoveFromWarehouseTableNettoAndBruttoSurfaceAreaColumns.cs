namespace SWAM.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class RemoveFromWarehouseTableNettoAndBruttoSurfaceAreaColumns : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Warehouses", "SurfaceAreaNetto");
            DropColumn("dbo.Warehouses", "SurfaceAreaBrutton");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Warehouses", "SurfaceAreaBrutton", c => c.Long(nullable: false));
            AddColumn("dbo.Warehouses", "SurfaceAreaNetto", c => c.Long(nullable: false));
        }
    }
}
