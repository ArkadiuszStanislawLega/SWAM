namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePhoneFromCharToString : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Phones", "PhoneNumber", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Phones", "PhoneNumber");
        }
    }
}
