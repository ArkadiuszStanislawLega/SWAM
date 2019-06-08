namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EmailModelDeleteIdUserAddUser : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "User_Id", "dbo.Users");
            DropIndex("dbo.Users", new[] { "User_Id" });
            DropColumn("dbo.Users", "User_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "User_Id", c => c.Int());
            CreateIndex("dbo.Users", "User_Id");
            AddForeignKey("dbo.Users", "User_Id", "dbo.Users", "Id");
        }
    }
}
