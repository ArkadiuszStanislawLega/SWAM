namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailAndPhoneTables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Emails",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressEmail = c.String(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Phones", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Emails", "User_Id", "dbo.Users");
            DropIndex("dbo.Phones", new[] { "User_Id" });
            DropIndex("dbo.Emails", new[] { "User_Id" });
            DropTable("dbo.Phones");
            DropTable("dbo.Emails");
        }
    }
}
