namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameColumnEmailToEmailAddressInCouriersTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Couriers", "EmailAddress", c => c.String());
            DropColumn("dbo.Couriers", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Couriers", "Email", c => c.String());
            DropColumn("dbo.Couriers", "EmailAddress");
        }
    }
}
