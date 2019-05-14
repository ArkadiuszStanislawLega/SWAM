namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Addresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        ApartmentNumber = c.String(),
                        PostCode = c.String(),
                        Courier_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Couriers", t => t.Courier_Id)
                .Index(t => t.Courier_Id);
            
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
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Couriers", t => t.Courier_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Warehouses", t => t.Warehouse_Id)
                .ForeignKey("dbo.ExternalSuppliers", t => t.ExternalSupplier_Id)
                .Index(t => t.Courier_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Warehouse_Id)
                .Index(t => t.ExternalSupplier_Id);
            
            CreateTable(
                "dbo.Couriers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Tin = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsPaid = c.Boolean(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(),
                        CustomerOrderStatus = c.Int(nullable: false),
                        ShipmentType = c.Int(nullable: false),
                        PaymentType = c.Int(nullable: false),
                        Courier_Id = c.Int(),
                        Customer_Id = c.Int(),
                        Warehouse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Couriers", t => t.Courier_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Warehouses", t => t.Warehouse_Id)
                .Index(t => t.Courier_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.Warehouse_Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        DeliveryAddress_Id = c.Int(),
                        ResidentAddress_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.DeliveryAddress_Id)
                .ForeignKey("dbo.Addresses", t => t.ResidentAddress_Id)
                .Index(t => t.DeliveryAddress_Id)
                .Index(t => t.ResidentAddress_Id);
            
            CreateTable(
                "dbo.CustomerOrderPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerOrder_Id = c.Int(),
                        Product_Id = c.Int(),
                        State_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerOrders", t => t.CustomerOrder_Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.States", t => t.State_Id)
                .Index(t => t.CustomerOrder_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.State_Id);
            
            CreateTable(
                "dbo.Products",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Weigth = c.Long(nullable: false),
                        Length = c.Long(nullable: false),
                        Width = c.Long(nullable: false),
                        Height = c.Long(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.WarehouseOrderPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Product_Id = c.Int(),
                        State_Id = c.Int(),
                        WarehouseOrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.Product_Id)
                .ForeignKey("dbo.States", t => t.State_Id)
                .ForeignKey("dbo.WarehouseOrders", t => t.WarehouseOrder_Id)
                .Index(t => t.Product_Id)
                .Index(t => t.State_Id)
                .Index(t => t.WarehouseOrder_Id);
            
            CreateTable(
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Available = c.Int(nullable: false),
                        Booked = c.Int(nullable: false),
                        Warehouse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Warehouses", t => t.Warehouse_Id)
                .Index(t => t.Warehouse_Id);
            
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Address_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .Index(t => t.Address_Id);
            
            CreateTable(
                "dbo.WarehouseOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(nullable: false),
                        WarehouseOrderStatus = c.Int(nullable: false),
                        ExternalSupplayer_Id = c.Int(),
                        Warehouse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExternalSuppliers", t => t.ExternalSupplayer_Id)
                .ForeignKey("dbo.Warehouses", t => t.Warehouse_Id)
                .Index(t => t.ExternalSupplayer_Id)
                .Index(t => t.Warehouse_Id);
            
            CreateTable(
                "dbo.ExternalSuppliers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Tin = c.String(),
                        Address_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .Index(t => t.Address_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.WarehouseOrderPositions", "WarehouseOrder_Id", "dbo.WarehouseOrders");
            DropForeignKey("dbo.WarehouseOrders", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.WarehouseOrders", "ExternalSupplayer_Id", "dbo.ExternalSuppliers");
            DropForeignKey("dbo.Contacts", "ExternalSupplier_Id", "dbo.ExternalSuppliers");
            DropForeignKey("dbo.ExternalSuppliers", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.WarehouseOrderPositions", "State_Id", "dbo.States");
            DropForeignKey("dbo.States", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.CustomerOrders", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.Contacts", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.Warehouses", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.CustomerOrderPositions", "State_Id", "dbo.States");
            DropForeignKey("dbo.WarehouseOrderPositions", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.CustomerOrderPositions", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.CustomerOrderPositions", "CustomerOrder_Id", "dbo.CustomerOrders");
            DropForeignKey("dbo.Customers", "ResidentAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Customers", "DeliveryAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.CustomerOrders", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Contacts", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.CustomerOrders", "Courier_Id", "dbo.Couriers");
            DropForeignKey("dbo.Contacts", "Courier_Id", "dbo.Couriers");
            DropForeignKey("dbo.Addresses", "Courier_Id", "dbo.Couriers");
            DropIndex("dbo.ExternalSuppliers", new[] { "Address_Id" });
            DropIndex("dbo.WarehouseOrders", new[] { "Warehouse_Id" });
            DropIndex("dbo.WarehouseOrders", new[] { "ExternalSupplayer_Id" });
            DropIndex("dbo.Warehouses", new[] { "Address_Id" });
            DropIndex("dbo.States", new[] { "Warehouse_Id" });
            DropIndex("dbo.WarehouseOrderPositions", new[] { "WarehouseOrder_Id" });
            DropIndex("dbo.WarehouseOrderPositions", new[] { "State_Id" });
            DropIndex("dbo.WarehouseOrderPositions", new[] { "Product_Id" });
            DropIndex("dbo.CustomerOrderPositions", new[] { "State_Id" });
            DropIndex("dbo.CustomerOrderPositions", new[] { "Product_Id" });
            DropIndex("dbo.CustomerOrderPositions", new[] { "CustomerOrder_Id" });
            DropIndex("dbo.Customers", new[] { "ResidentAddress_Id" });
            DropIndex("dbo.Customers", new[] { "DeliveryAddress_Id" });
            DropIndex("dbo.CustomerOrders", new[] { "Warehouse_Id" });
            DropIndex("dbo.CustomerOrders", new[] { "Customer_Id" });
            DropIndex("dbo.CustomerOrders", new[] { "Courier_Id" });
            DropIndex("dbo.Contacts", new[] { "ExternalSupplier_Id" });
            DropIndex("dbo.Contacts", new[] { "Warehouse_Id" });
            DropIndex("dbo.Contacts", new[] { "Customer_Id" });
            DropIndex("dbo.Contacts", new[] { "Courier_Id" });
            DropIndex("dbo.Addresses", new[] { "Courier_Id" });
            DropTable("dbo.ExternalSuppliers");
            DropTable("dbo.WarehouseOrders");
            DropTable("dbo.Warehouses");
            DropTable("dbo.States");
            DropTable("dbo.WarehouseOrderPositions");
            DropTable("dbo.Products");
            DropTable("dbo.CustomerOrderPositions");
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerOrders");
            DropTable("dbo.Couriers");
            DropTable("dbo.Contacts");
            DropTable("dbo.Addresses");
        }
    }
}
