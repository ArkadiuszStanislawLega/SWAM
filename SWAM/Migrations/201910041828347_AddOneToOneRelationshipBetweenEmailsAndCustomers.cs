namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOneToOneRelationshipBetweenEmailsAndCustomers : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsersEmailAddresses", "Id", "dbo.EmailAddresses");
            DropForeignKey("dbo.CustomersEmailAddresses", "Id", "dbo.EmailAddresses");
            DropPrimaryKey("dbo.EmailAddresses");
            AlterColumn("dbo.EmailAddresses", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.EmailAddresses", "Id");
            AddForeignKey("dbo.CustomersEmailAddresses", "Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.UsersEmailAddresses", "Id", "dbo.EmailAddresses", "Id");
            AddForeignKey("dbo.CustomersEmailAddresses", "Id", "dbo.EmailAddresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomersEmailAddresses", "Id", "dbo.EmailAddresses");
            DropForeignKey("dbo.UsersEmailAddresses", "Id", "dbo.EmailAddresses");
            DropForeignKey("dbo.CustomersEmailAddresses", "Id", "dbo.Customers");
            DropPrimaryKey("dbo.EmailAddresses");
            AlterColumn("dbo.EmailAddresses", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.EmailAddresses", "Id");
            AddForeignKey("dbo.CustomersEmailAddresses", "Id", "dbo.EmailAddresses", "Id");
            AddForeignKey("dbo.UsersEmailAddresses", "Id", "dbo.EmailAddresses", "Id");
        }
    }
}
