namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCustomerTheClientDoesNotInheritFromPerson : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Customers", "Id", "dbo.People");
            DropForeignKey("dbo.CustomerOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerResidentalAddress", "Id", "dbo.Customers");
            DropIndex("dbo.Customers", new[] { "Id" });
            DropPrimaryKey("dbo.Customers");
            AddColumn("dbo.Customers", "Name", c => c.String());
            AlterColumn("dbo.Customers", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Customers", "Id");
            AddForeignKey("dbo.CustomerOrders", "CustomerId", "dbo.Customers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.CustomerResidentalAddress", "Id", "dbo.Customers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerResidentalAddress", "Id", "dbo.Customers");
            DropForeignKey("dbo.CustomerOrders", "CustomerId", "dbo.Customers");
            DropPrimaryKey("dbo.Customers");
            AlterColumn("dbo.Customers", "Id", c => c.Int(nullable: false));
            DropColumn("dbo.Customers", "Name");
            AddPrimaryKey("dbo.Customers", "Id");
            CreateIndex("dbo.Customers", "Id");
            AddForeignKey("dbo.CustomerResidentalAddress", "Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.CustomerOrders", "CustomerId", "dbo.Customers", "Id");
            AddForeignKey("dbo.Customers", "Id", "dbo.People", "Id");
        }
    }
}
