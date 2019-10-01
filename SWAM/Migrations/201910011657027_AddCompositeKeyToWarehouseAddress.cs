namespace SWAM.Migrations
{
    using System.Data.Entity.Migrations;
    
    public partial class AddCompositeKeyToWarehouseAddress : DbMigration
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
