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
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Contacts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Email = c.String(),
                        Warehouse_Id = c.Int(),
                        Customer_Id = c.Int(),
                        ExternalSupplayer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Warehouses", t => t.Warehouse_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.ExternalSupplayers", t => t.ExternalSupplayer_Id)
                .Index(t => t.Warehouse_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.ExternalSupplayer_Id);
            
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
                "dbo.ExternalSupplayers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        ConterpartyId = c.Int(nullable: false),
                        Address_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Address_Id)
                .Index(t => t.Address_Id);
            
            CreateTable(
                "dbo.OrderToWarehouses",
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
                .ForeignKey("dbo.ExternalSupplayers", t => t.ExternalSupplayer_Id)
                .ForeignKey("dbo.Warehouses", t => t.Warehouse_Id)
                .Index(t => t.ExternalSupplayer_Id)
                .Index(t => t.Warehouse_Id);
            
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
                        SellingPrice = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderToWarehouses", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.OrderToWarehouses", "ExternalSupplayer_Id", "dbo.ExternalSupplayers");
            DropForeignKey("dbo.Contacts", "ExternalSupplayer_Id", "dbo.ExternalSupplayers");
            DropForeignKey("dbo.ExternalSupplayers", "Address_Id", "dbo.Addresses");
            DropForeignKey("dbo.Customers", "ResidentAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Customers", "DeliveryAddress_Id", "dbo.Addresses");
            DropForeignKey("dbo.Contacts", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.Contacts", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.Warehouses", "Address_Id", "dbo.Addresses");
            DropIndex("dbo.OrderToWarehouses", new[] { "Warehouse_Id" });
            DropIndex("dbo.OrderToWarehouses", new[] { "ExternalSupplayer_Id" });
            DropIndex("dbo.ExternalSupplayers", new[] { "Address_Id" });
            DropIndex("dbo.Customers", new[] { "ResidentAddress_Id" });
            DropIndex("dbo.Customers", new[] { "DeliveryAddress_Id" });
            DropIndex("dbo.Warehouses", new[] { "Address_Id" });
            DropIndex("dbo.Contacts", new[] { "ExternalSupplayer_Id" });
            DropIndex("dbo.Contacts", new[] { "Customer_Id" });
            DropIndex("dbo.Contacts", new[] { "Warehouse_Id" });
            DropTable("dbo.Products");
            DropTable("dbo.OrderToWarehouses");
            DropTable("dbo.ExternalSupplayers");
            DropTable("dbo.Customers");
            DropTable("dbo.Warehouses");
            DropTable("dbo.Contacts");
            DropTable("dbo.Addresses");
        }
    }
}
