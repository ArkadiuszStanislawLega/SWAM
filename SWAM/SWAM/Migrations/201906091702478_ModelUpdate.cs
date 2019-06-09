namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModelUpdate : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Emails", "UserId", "dbo.Users");
            DropForeignKey("dbo.Phones", "UserId", "dbo.Users");
            DropIndex("dbo.Emails", new[] { "UserId" });
            DropIndex("dbo.Phones", new[] { "UserId" });
            RenameColumn(table: "dbo.Emails", name: "UserId", newName: "User_Id");
            RenameColumn(table: "dbo.Phones", name: "UserId", newName: "User_Id");
            AlterColumn("dbo.Emails", "User_Id", c => c.Int());
            AlterColumn("dbo.Phones", "User_Id", c => c.Int());
            CreateIndex("dbo.Emails", "User_Id");
            CreateIndex("dbo.Phones", "User_Id");
            AddForeignKey("dbo.Emails", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Phones", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Phones", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Emails", "User_Id", "dbo.Users");
            DropIndex("dbo.Phones", new[] { "User_Id" });
            DropIndex("dbo.Emails", new[] { "User_Id" });
            AlterColumn("dbo.Phones", "User_Id", c => c.Int(nullable: false));
            AlterColumn("dbo.Emails", "User_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Phones", name: "User_Id", newName: "UserId");
            RenameColumn(table: "dbo.Emails", name: "User_Id", newName: "UserId");
            CreateIndex("dbo.Phones", "UserId");
            CreateIndex("dbo.Emails", "UserId");
            AddForeignKey("dbo.Phones", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Emails", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
