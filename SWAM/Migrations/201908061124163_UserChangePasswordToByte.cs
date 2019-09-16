namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserChangePasswordToByte : DbMigration
    {
        public override void Up()
        {

            AddColumn("dbo.Users", "PasswordTmp", c => c.Binary());
            Sql("Update dbo.Users SET PasswordTmp = Convert(varbinary, Password)");
            DropColumn("dbo.Users", "Password");
            RenameColumn("dbo.Users", "PasswordTmp", "Password");
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Users", "Password", c => c.String());
        }
    }
}
