namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddEmailsToUserAndCustomer : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Emails", "Customer_Id", c => c.Int());
            CreateIndex("dbo.Emails", "Customer_Id");
            AddForeignKey("dbo.Emails", "Customer_Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Emails", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Emails", new[] { "Customer_Id" });
            DropColumn("dbo.Emails", "Customer_Id");
        }
    }
}
