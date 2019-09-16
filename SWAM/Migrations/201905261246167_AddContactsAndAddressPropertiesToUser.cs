namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddContactsAndAddressPropertiesToUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Contacts", "User_Id", c => c.Int());
            AddColumn("dbo.Customers", "Surname", c => c.String());
            AddColumn("dbo.Users", "ResidentAddress_Id", c => c.Int());
            CreateIndex("dbo.Contacts", "User_Id");
            CreateIndex("dbo.Users", "ResidentAddress_Id");
            AddForeignKey("dbo.Contacts", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "ResidentAddress_Id", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "ResidentAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Contacts", "User_Id", "dbo.Users");
            DropIndex("dbo.Users", new[] { "ResidentAddress_Id" });
            DropIndex("dbo.Contacts", new[] { "User_Id" });
            DropColumn("dbo.Users", "ResidentAddress_Id");
            DropColumn("dbo.Customers", "Surname");
            DropColumn("dbo.Contacts", "User_Id");
        }
    }
}
