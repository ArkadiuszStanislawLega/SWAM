namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserAddPasswordSalt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Users", "PasswordSalt", c => c.Binary());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Users", "PasswordSalt");
        }
    }
}
