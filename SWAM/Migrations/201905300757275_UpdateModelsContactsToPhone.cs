namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateModelsContactsToPhone : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Contacts", "Courier_Id", "dbo.Couriers");
            DropForeignKey("dbo.Contacts", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Contacts", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.Contacts", "ExternalSupplier_Id", "dbo.ExternalSuppliers");
            DropIndex("dbo.Contacts", new[] { "Courier_Id" });
            DropIndex("dbo.Contacts", new[] { "Customer_Id" });
            DropIndex("dbo.Contacts", new[] { "Warehouse_Id" });
            DropIndex("dbo.Contacts", new[] { "ExternalSupplier_Id" });
            DropIndex("dbo.Contacts", new[] { "User_Id" });
            AddColumn("dbo.Phones", "Customer_Id", c => c.Int());
            AddColumn("dbo.Phones", "Warehouse_Id", c => c.Int());
            AddColumn("dbo.Phones", "ExternalSupplier_Id", c => c.Int());
            AddColumn("dbo.Phones", "Courier_Id", c => c.Int());
            CreateIndex("dbo.Phones", "Customer_Id");
            CreateIndex("dbo.Phones", "Warehouse_Id");
            CreateIndex("dbo.Phones", "ExternalSupplier_Id");
            CreateIndex("dbo.Phones", "Courier_Id");
            AddForeignKey("dbo.Phones", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Phones", "Warehouse_Id", "dbo.Warehouses", "Id");
            AddForeignKey("dbo.Phones", "ExternalSupplier_Id", "dbo.ExternalSuppliers", "Id");
            AddForeignKey("dbo.Phones", "Courier_Id", "dbo.Couriers", "Id");
            DropTable("dbo.Contacts");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Courier_Id = c.Int(),
                        Customer_Id = c.Int(),
                        Warehouse_Id = c.Int(),
                        ExternalSupplier_Id = c.Int(),
                        User_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Phones", "Courier_Id", "dbo.Couriers");
            DropForeignKey("dbo.Phones", "ExternalSupplier_Id", "dbo.ExternalSuppliers");
            DropForeignKey("dbo.Phones", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.Phones", "Customer_Id", "dbo.Customers");
            DropIndex("dbo.Phones", new[] { "Courier_Id" });
            DropIndex("dbo.Phones", new[] { "ExternalSupplier_Id" });
            DropIndex("dbo.Phones", new[] { "Warehouse_Id" });
            DropIndex("dbo.Phones", new[] { "Customer_Id" });
            DropColumn("dbo.Phones", "Courier_Id");
            DropColumn("dbo.Phones", "ExternalSupplier_Id");
            DropColumn("dbo.Phones", "Warehouse_Id");
            DropColumn("dbo.Phones", "Customer_Id");
            CreateIndex("dbo.Contacts", "User_Id");
            CreateIndex("dbo.Contacts", "ExternalSupplier_Id");
            CreateIndex("dbo.Contacts", "Warehouse_Id");
            CreateIndex("dbo.Contacts", "Customer_Id");
            CreateIndex("dbo.Contacts", "Courier_Id");
            AddForeignKey("dbo.Contacts", "ExternalSupplier_Id", "dbo.ExternalSuppliers", "Id");
            AddForeignKey("dbo.Contacts", "Warehouse_Id", "dbo.Warehouses", "Id");
            AddForeignKey("dbo.Contacts", "Customer_Id", "dbo.Customers", "Id");
            AddForeignKey("dbo.Contacts", "Courier_Id", "dbo.Couriers", "Id");
        }
    }
}
