namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropRelationshipBetweenEmailsAndCustomers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomersEmailAddresses", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.CustomersEmailAddresses", new[] { "Customer_Id" });
            DropColumn("dbo.CustomersEmailAddresses", "Customer_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.CustomersEmailAddresses", "Customer_Id", c => c.Int());
            CreateIndex("dbo.CustomersEmailAddresses", "Customer_Id");
            AddForeignKey("dbo.CustomersEmailAddresses", "Customer_Id", "dbo.Customers", "Id");
        }
    }
}
