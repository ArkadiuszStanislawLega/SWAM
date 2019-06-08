namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PhonesModelUpdateDeleteUserId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Phones", "UserId", "dbo.Users");
            DropIndex("dbo.Phones", new[] { "UserId" });
            RenameColumn(table: "dbo.Phones", name: "UserId", newName: "User_Id");
            AlterColumn("dbo.Phones", "User_Id", c => c.Int());
            CreateIndex("dbo.Phones", "User_Id");
            AddForeignKey("dbo.Phones", "User_Id", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Phones", "User_Id", "dbo.Users");
            DropIndex("dbo.Phones", new[] { "User_Id" });
            AlterColumn("dbo.Phones", "User_Id", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Phones", name: "User_Id", newName: "UserId");
            CreateIndex("dbo.Phones", "UserId");
            AddForeignKey("dbo.Phones", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
