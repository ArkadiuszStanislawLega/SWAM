namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WarehouseAddPropertyLength : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Warehouses", "Length", c => c.Long(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Warehouses", "Length");
        }
    }
}
