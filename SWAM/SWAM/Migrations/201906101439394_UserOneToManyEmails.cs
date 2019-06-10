namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserOneToManyEmails : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Emails", "User_Id", "dbo.Users");
            DropIndex("dbo.Emails", new[] { "User_Id" });
            RenameColumn(table: "dbo.Emails", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Emails", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Emails", "UserId");
            AddForeignKey("dbo.Emails", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emails", "UserId", "dbo.Users");
            DropIndex("dbo.Emails", new[] { "UserId" });
            AlterColumn("dbo.Emails", "UserId", c => c.Int());
            RenameColumn(table: "dbo.Emails", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.Emails", "User_Id");
            AddForeignKey("dbo.Emails", "User_Id", "dbo.Users", "Id");
        }
    }
}
