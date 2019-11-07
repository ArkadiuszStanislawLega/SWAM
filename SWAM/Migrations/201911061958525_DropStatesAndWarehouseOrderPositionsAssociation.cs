namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropStatesAndWarehouseOrderPositionsAssociation : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.WarehouseOrderPositions", "State_Id", "dbo.States");
            DropIndex("dbo.WarehouseOrderPositions", new[] { "State_Id" });
            DropColumn("dbo.WarehouseOrderPositions", "State_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.WarehouseOrderPositions", "State_Id", c => c.Int());
            CreateIndex("dbo.WarehouseOrderPositions", "State_Id");
            AddForeignKey("dbo.WarehouseOrderPositions", "State_Id", "dbo.States", "Id");
        }
    }
}
