namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AccessUsersToWarehouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TypeOfAccess = c.Int(nullable: false),
                        DateOfGrantingAccess = c.DateTime(nullable: false),
                        DateOfExpiredAcces = c.DateTime(),
                        Administrator_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                        Warehouse_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Administrator_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Warehouses", t => t.Warehouse_Id, cascadeDelete: true)
                .Index(t => t.Administrator_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Warehouse_Id);
            
            CreateTable(
                "dbo.People",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmailAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AddressEmail = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Messages",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ContentOfMessage = c.String(),
                        TitleOfMessage = c.String(),
                        PostDate = c.DateTime(nullable: false),
                        DateOfReading = c.DateTime(),
                        IsReaded = c.Boolean(nullable: false),
                        IsDeletedBySender = c.Boolean(nullable: false),
                        IsDeletedByReceiver = c.Boolean(nullable: false),
                        Receiver_Id = c.Int(),
                        Sender_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Receiver_Id)
                .ForeignKey("dbo.Users", t => t.Sender_Id)
                .Index(t => t.Receiver_Id)
                .Index(t => t.Sender_Id);
            
            CreateTable(
                "dbo.Phones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(),
                        Note = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Height = c.Long(nullable: false),
                        Width = c.Long(nullable: false),
                        Length = c.Long(nullable: false),
                        SurfaceAreaNetto = c.Long(nullable: false),
                        SurfaceAreaBrutton = c.Long(nullable: false),
                        AcceptableWeight = c.Long(nullable: false),
                        WarehouseAddressId = c.Int(nullable: false),
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
                        Customer_Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                        Warehouse_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Couriers", t => t.Courier_Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .ForeignKey("dbo.Warehouses", t => t.Warehouse_Id, cascadeDelete: true)
                .Index(t => t.Courier_Id)
                .Index(t => t.Customer_Id)
                .Index(t => t.User_Id)
                .Index(t => t.Warehouse_Id);
            
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
                "dbo.States",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Available = c.Int(nullable: false),
                        Booked = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        WarehouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseId, cascadeDelete: true)
                .Index(t => t.ProductId)
                .Index(t => t.WarehouseId);
            
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
                "dbo.WarehouseOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        OrderDate = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(),
                        WarehouseOrderStatus = c.Int(nullable: false),
                        ExternalSupplayer_Id = c.Int(nullable: false),
                        Warehouse_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExternalSuppliers", t => t.ExternalSupplayer_Id)
                .ForeignKey("dbo.Warehouses", t => t.Warehouse_Id, cascadeDelete: true)
                .Index(t => t.ExternalSupplayer_Id)
                .Index(t => t.Warehouse_Id);
            
            CreateTable(
                "dbo.WarehouseAddress",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Warehouse_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Id)
                .ForeignKey("dbo.Warehouses", t => t.Warehouse_Id)
                .Index(t => t.Id)
                .Index(t => t.Warehouse_Id);
            
            CreateTable(
                "dbo.Couriers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Tin = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CouriersPhones", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Surname = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ExternalSuppliers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Tin = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Password = c.Binary(),
                        PasswordSalt = c.Binary(),
                        Permissions = c.Int(nullable: false),
                        StatusOfUserAccount = c.Int(nullable: false),
                        DateOfCreate = c.DateTime(nullable: false),
                        ExpiryDateOfTheBlockade = c.DateTime(),
                        DateOfExpiryOfTheAccount = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.People", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.UsersEmailAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmailAddresses", t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.UsersPhones",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        User_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Phones", t => t.Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.CouriersPhones",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Phones", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.CustomersDeliveryAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Customer_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Customer_Id)
                .Index(t => t.Id)
                .Index(t => t.Customer_Id);
            
            CreateTable(
                "dbo.CustomersEmailAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmailAddresses", t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.CustomersPhones",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Phones", t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.CusomtersResidentalAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Addresses", t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CusomtersResidentalAddresses", "Id", "dbo.Customers");
            DropForeignKey("dbo.CusomtersResidentalAddresses", "Id", "dbo.Addresses");
            DropForeignKey("dbo.CustomersPhones", "Id", "dbo.Customers");
            DropForeignKey("dbo.CustomersPhones", "Id", "dbo.Phones");
            DropForeignKey("dbo.CustomersEmailAddresses", "Id", "dbo.Customers");
            DropForeignKey("dbo.CustomersEmailAddresses", "Id", "dbo.EmailAddresses");
            DropForeignKey("dbo.CustomersDeliveryAddresses", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.CustomersDeliveryAddresses", "Id", "dbo.Addresses");
            DropForeignKey("dbo.CouriersPhones", "Id", "dbo.Phones");
            DropForeignKey("dbo.UsersPhones", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UsersPhones", "Id", "dbo.Phones");
            DropForeignKey("dbo.UsersEmailAddresses", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UsersEmailAddresses", "Id", "dbo.EmailAddresses");
            DropForeignKey("dbo.Users", "Id", "dbo.People");
            DropForeignKey("dbo.ExternalSuppliers", "Id", "dbo.People");
            DropForeignKey("dbo.Customers", "Id", "dbo.People");
            DropForeignKey("dbo.Couriers", "Id", "dbo.CouriersPhones");
            DropForeignKey("dbo.WarehouseAddress", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.WarehouseAddress", "Id", "dbo.Addresses");
            DropForeignKey("dbo.AccessUsersToWarehouses", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.WarehouseOrders", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.CustomerOrders", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.CustomerOrders", "User_Id", "dbo.Users");
            DropForeignKey("dbo.WarehouseOrderPositions", "WarehouseOrder_Id", "dbo.WarehouseOrders");
            DropForeignKey("dbo.WarehouseOrders", "ExternalSupplayer_Id", "dbo.ExternalSuppliers");
            DropForeignKey("dbo.WarehouseOrderPositions", "State_Id", "dbo.States");
            DropForeignKey("dbo.WarehouseOrderPositions", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.States", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.States", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CustomerOrderPositions", "State_Id", "dbo.States");
            DropForeignKey("dbo.CustomerOrderPositions", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.CustomerOrderPositions", "CustomerOrder_Id", "dbo.CustomerOrders");
            DropForeignKey("dbo.CustomerOrders", "Customer_Id", "dbo.Customers");
            DropForeignKey("dbo.CustomerOrders", "Courier_Id", "dbo.Couriers");
            DropForeignKey("dbo.AccessUsersToWarehouses", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AccessUsersToWarehouses", "Administrator_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "Sender_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "Receiver_Id", "dbo.Users");
            DropIndex("dbo.CusomtersResidentalAddresses", new[] { "Id" });
            DropIndex("dbo.CustomersPhones", new[] { "Id" });
            DropIndex("dbo.CustomersEmailAddresses", new[] { "Id" });
            DropIndex("dbo.CustomersDeliveryAddresses", new[] { "Customer_Id" });
            DropIndex("dbo.CustomersDeliveryAddresses", new[] { "Id" });
            DropIndex("dbo.CouriersPhones", new[] { "Id" });
            DropIndex("dbo.UsersPhones", new[] { "User_Id" });
            DropIndex("dbo.UsersPhones", new[] { "Id" });
            DropIndex("dbo.UsersEmailAddresses", new[] { "User_Id" });
            DropIndex("dbo.UsersEmailAddresses", new[] { "Id" });
            DropIndex("dbo.Users", new[] { "Id" });
            DropIndex("dbo.ExternalSuppliers", new[] { "Id" });
            DropIndex("dbo.Customers", new[] { "Id" });
            DropIndex("dbo.Couriers", new[] { "Id" });
            DropIndex("dbo.WarehouseAddress", new[] { "Warehouse_Id" });
            DropIndex("dbo.WarehouseAddress", new[] { "Id" });
            DropIndex("dbo.WarehouseOrders", new[] { "Warehouse_Id" });
            DropIndex("dbo.WarehouseOrders", new[] { "ExternalSupplayer_Id" });
            DropIndex("dbo.WarehouseOrderPositions", new[] { "WarehouseOrder_Id" });
            DropIndex("dbo.WarehouseOrderPositions", new[] { "State_Id" });
            DropIndex("dbo.WarehouseOrderPositions", new[] { "Product_Id" });
            DropIndex("dbo.States", new[] { "WarehouseId" });
            DropIndex("dbo.States", new[] { "ProductId" });
            DropIndex("dbo.CustomerOrderPositions", new[] { "State_Id" });
            DropIndex("dbo.CustomerOrderPositions", new[] { "Product_Id" });
            DropIndex("dbo.CustomerOrderPositions", new[] { "CustomerOrder_Id" });
            DropIndex("dbo.CustomerOrders", new[] { "Warehouse_Id" });
            DropIndex("dbo.CustomerOrders", new[] { "User_Id" });
            DropIndex("dbo.CustomerOrders", new[] { "Customer_Id" });
            DropIndex("dbo.CustomerOrders", new[] { "Courier_Id" });
            DropIndex("dbo.Messages", new[] { "Sender_Id" });
            DropIndex("dbo.Messages", new[] { "Receiver_Id" });
            DropIndex("dbo.AccessUsersToWarehouses", new[] { "Warehouse_Id" });
            DropIndex("dbo.AccessUsersToWarehouses", new[] { "User_Id" });
            DropIndex("dbo.AccessUsersToWarehouses", new[] { "Administrator_Id" });
            DropTable("dbo.CusomtersResidentalAddresses");
            DropTable("dbo.CustomersPhones");
            DropTable("dbo.CustomersEmailAddresses");
            DropTable("dbo.CustomersDeliveryAddresses");
            DropTable("dbo.CouriersPhones");
            DropTable("dbo.UsersPhones");
            DropTable("dbo.UsersEmailAddresses");
            DropTable("dbo.Users");
            DropTable("dbo.ExternalSuppliers");
            DropTable("dbo.Customers");
            DropTable("dbo.Couriers");
            DropTable("dbo.WarehouseAddress");
            DropTable("dbo.WarehouseOrders");
            DropTable("dbo.WarehouseOrderPositions");
            DropTable("dbo.States");
            DropTable("dbo.Products");
            DropTable("dbo.CustomerOrderPositions");
            DropTable("dbo.Addresses");
            DropTable("dbo.CustomerOrders");
            DropTable("dbo.Warehouses");
            DropTable("dbo.Phones");
            DropTable("dbo.Messages");
            DropTable("dbo.EmailAddresses");
            DropTable("dbo.People");
            DropTable("dbo.AccessUsersToWarehouses");
        }
    }
}
