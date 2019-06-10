namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UserOneToManyPhones : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Phones", "User_Id", "dbo.Users");
            DropIndex("dbo.Phones", new[] { "User_Id" });
            RenameColumn(table: "dbo.Phones", name: "User_Id", newName: "UserId");
            AlterColumn("dbo.Phones", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.Phones", "UserId");
            AddForeignKey("dbo.Phones", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Phones", "UserId", "dbo.Users");
            DropIndex("dbo.Phones", new[] { "UserId" });
            AlterColumn("dbo.Phones", "UserId", c => c.Int());
            RenameColumn(table: "dbo.Phones", name: "UserId", newName: "User_Id");
            CreateIndex("dbo.Phones", "User_Id");
            AddForeignKey("dbo.Phones", "User_Id", "dbo.Users", "Id");
        }
    }
}
