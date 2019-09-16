namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPropertiesToUserAccount : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "DateOfCreate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "ExpiryDateOfTheBlockade", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "DateOfExpiryOfTheAccount", c => c.DateTime(nullable: false));
            AddColumn("dbo.Users", "StatusOfUserAccount", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "StatusOfUserAccount");
            DropColumn("dbo.Users", "DateOfExpiryOfTheAccount");
            DropColumn("dbo.Users", "ExpiryDateOfTheBlockade");
            DropColumn("dbo.Users", "DateOfCreate");
        }
    }
}
