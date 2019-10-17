namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailToCouriersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Couriers", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Couriers", "Email");
        }
    }
}
