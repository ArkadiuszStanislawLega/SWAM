namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpgdateDataUserAccount : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Users", "ExpiryDateOfTheBlockade", c => c.DateTime());
            AlterColumn("dbo.Users", "DateOfExpiryOfTheAccount", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "DateOfExpiryOfTheAccount", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Users", "ExpiryDateOfTheBlockade", c => c.DateTime(nullable: false));
        }
    }
}
