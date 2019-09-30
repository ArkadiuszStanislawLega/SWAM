namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModels : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.States", "Quantity", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.States", "Quantity");
        }
    }
}
