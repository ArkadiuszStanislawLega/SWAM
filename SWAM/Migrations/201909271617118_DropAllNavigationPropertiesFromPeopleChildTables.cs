namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropAllNavigationPropertiesFromPeopleChildTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Addresses", "Person_Id", "dbo.People");
            DropForeignKey("dbo.Phones", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Phones", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Phones", "ExternalSupplier_Id", "dbo.ExternalSuppliers");
            DropForeignKey("dbo.Couriers", "CourierAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Customers", "DeliveryAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Customers", "ResidentAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.ExternalSuppliers", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.Phones", new[] { "Customer_Id" });
            DropIndex("dbo.Phones", new[] { "User_Id" });
            DropIndex("dbo.Phones", new[] { "ExternalSupplier_Id" });
            DropIndex("dbo.Addresses", new[] { "Person_Id" });
            DropIndex("dbo.Couriers", new[] { "CourierAddress_Id" });
            DropIndex("dbo.Customers", new[] { "DeliveryAddress_Id" });
            DropIndex("dbo.Customers", new[] { "ResidentAddress_Id" });
            DropIndex("dbo.ExternalSuppliers", new[] { "Address_Id" });
            DropColumn("dbo.Couriers", "CourierAddress_Id");
            DropColumn("dbo.Customers", "DeliveryAddress_Id");
            DropColumn("dbo.Customers", "ResidentAddress_Id");
            DropColumn("dbo.ExternalSuppliers", "Address_Id");
            DropColumn("dbo.Phones", "Customer_Id");
            DropColumn("dbo.Phones", "User_Id");
            DropColumn("dbo.Phones", "ExternalSupplier_Id");
            DropColumn("dbo.Addresses", "Person_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Addresses", "Person_Id", c => c.Int());
            AddColumn("dbo.Phones", "ExternalSupplier_Id", c => c.Int());
            AddColumn("dbo.Phones", "User_Id", c => c.Int());
            AddColumn("dbo.Phones", "Customer_Id", c => c.Int());
            AddColumn("dbo.ExternalSuppliers", "Address_Id", c => c.Int());
            AddColumn("dbo.Customers", "ResidentAddress_Id", c => c.Int());
            AddColumn("dbo.Customers", "DeliveryAddress_Id", c => c.Int());
            AddColumn("dbo.Couriers", "CourierAddress_Id", c => c.Int());
            CreateIndex("dbo.ExternalSuppliers", "Address_Id");
            CreateIndex("dbo.Customers", "ResidentAddress_Id");
            CreateIndex("dbo.Customers", "DeliveryAddress_Id");
            CreateIndex("dbo.Couriers", "CourierAddress_Id");
            CreateIndex("dbo.Addresses", "Person_Id");
            CreateIndex("dbo.Phones", "ExternalSupplier_Id");
            CreateIndex("dbo.Phones", "User_Id");
            CreateIndex("dbo.Phones", "Customer_Id");
            AddForeignKey("dbo.ExternalSuppliers", "Address_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Customers", "ResidentAddress_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Customers", "DeliveryAddress_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Couriers", "CourierAddress_Id", "dbo.Addresses", "Id");
            AddForeignKey("dbo.Phones", "ExternalSupplier_Id", "dbo.ExternalSuppliers", "Id");
            AddForeignKey("dbo.Phones", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.Phones", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Addresses", "Person_Id", "dbo.People", "Id");
        }
    }
}
