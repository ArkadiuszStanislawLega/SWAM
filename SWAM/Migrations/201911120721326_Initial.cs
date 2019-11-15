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
                        Name = c.String(nullable: false, maxLength: 60),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "IX_FirstName");
            
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
                "dbo.WarehouseOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsPaid = c.Boolean(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(),
                        WarehouseId = c.Int(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        UserReceivedOrderId = c.Int(),
                        ExternalSupplayerId = c.Int(nullable: false),
                        WarehouseOrderStatus = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.ExternalSuppliers", t => t.ExternalSupplayerId)
                .ForeignKey("dbo.Users", t => t.UserReceivedOrderId)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseId, cascadeDelete: true)
                .Index(t => t.WarehouseId)
                .Index(t => t.CreatorId)
                .Index(t => t.UserReceivedOrderId)
                .Index(t => t.ExternalSupplayerId);
            
            CreateTable(
                "dbo.ExternalSuppliersAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Country = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        ApartmentNumber = c.String(),
                        PostCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExternalSuppliers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ExternalSupplierEmailAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        AddressEmail = c.String(),
                        Note = c.String(),
                        ExternalSupplierId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExternalSuppliers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.ExternalSupplierPhones",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PhoneNumber = c.String(),
                        Note = c.String(),
                        ExternalSupplier_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ExternalSuppliers", t => t.ExternalSupplier_Id)
                .Index(t => t.ExternalSupplier_Id);
            
            CreateTable(
                "dbo.WarehouseOrderPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Quantity = c.Int(nullable: false),
                        ProductId = c.Int(nullable: false),
                        WarehouseOrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.WarehouseOrders", t => t.WarehouseOrder_Id)
                .Index(t => t.ProductId)
                .Index(t => t.WarehouseOrder_Id);
            
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
                "dbo.CustomerOrderPositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ProductId = c.Int(nullable: false),
                        Quantity = c.Int(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CustomerOrder_Id = c.Int(),
                        State_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerOrders", t => t.CustomerOrder_Id)
                .ForeignKey("dbo.Products", t => t.ProductId, cascadeDelete: true)
                .ForeignKey("dbo.States", t => t.State_Id)
                .Index(t => t.ProductId)
                .Index(t => t.CustomerOrder_Id)
                .Index(t => t.State_Id);
            
            CreateTable(
                "dbo.CustomerOrders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IsPaid = c.Boolean(nullable: false),
                        OrderDate = c.DateTime(nullable: false),
                        DeliveryDate = c.DateTime(),
                        WarehouseId = c.Int(nullable: false),
                        CreatorId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        CourierId = c.Int(),
                        CustomerOrderStatus = c.Int(nullable: false),
                        ShipmentType = c.Int(nullable: false),
                        PaymentType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Couriers", t => t.CourierId)
                .ForeignKey("dbo.Users", t => t.CreatorId)
                .ForeignKey("dbo.Customers", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.Warehouses", t => t.WarehouseId, cascadeDelete: true)
                .Index(t => t.WarehouseId)
                .Index(t => t.CreatorId)
                .Index(t => t.CustomerId)
                .Index(t => t.CourierId);
            
            CreateTable(
                "dbo.Customers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Surname = c.String(),
                        Phone = c.String(),
                        EmailAddress = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.CustomerResidentalAddress",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Country = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        ApartmentNumber = c.String(),
                        PostCode = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customers", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.CustomerOrederDeliveryAddress",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Country = c.String(),
                        City = c.String(),
                        Street = c.String(),
                        HouseNumber = c.String(),
                        ApartmentNumber = c.String(),
                        PostCode = c.String(),
                        CustomerOrder_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.CustomerOrders", t => t.CustomerOrder_Id)
                .Index(t => t.CustomerOrder_Id);
            
            CreateTable(
                "dbo.Warehouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Height = c.Long(nullable: false),
                        Width = c.Long(nullable: false),
                        Length = c.Long(nullable: false),
                        AcceptableWeight = c.Long(nullable: false),
                        WarehouseAddressId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.Couriers",
                c => new
                    {
                        Id = c.Int(nullable: false),
                        Tin = c.String(),
                        Phone = c.String(),
                        EmailAddress = c.String(),
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
                        Courier_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Phones", t => t.Id)
                .ForeignKey("dbo.Couriers", t => t.Courier_Id)
                .Index(t => t.Id)
                .Index(t => t.Courier_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CouriersPhones", "Courier_Id", "dbo.Couriers");
            DropForeignKey("dbo.CouriersPhones", "Id", "dbo.Phones");
            DropForeignKey("dbo.UsersPhones", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UsersPhones", "Id", "dbo.Phones");
            DropForeignKey("dbo.UsersEmailAddresses", "User_Id", "dbo.Users");
            DropForeignKey("dbo.UsersEmailAddresses", "Id", "dbo.EmailAddresses");
            DropForeignKey("dbo.Users", "Id", "dbo.People");
            DropForeignKey("dbo.WarehouseAddress", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.WarehouseAddress", "Id", "dbo.Addresses");
            DropForeignKey("dbo.ExternalSuppliers", "Id", "dbo.People");
            DropForeignKey("dbo.Couriers", "Id", "dbo.People");
            DropForeignKey("dbo.AccessUsersToWarehouses", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.AccessUsersToWarehouses", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AccessUsersToWarehouses", "Administrator_Id", "dbo.Users");
            DropForeignKey("dbo.WarehouseOrders", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.WarehouseOrders", "UserReceivedOrderId", "dbo.Users");
            DropForeignKey("dbo.WarehouseOrderPositions", "WarehouseOrder_Id", "dbo.WarehouseOrders");
            DropForeignKey("dbo.WarehouseOrderPositions", "ProductId", "dbo.Products");
            DropForeignKey("dbo.States", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.States", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CustomerOrderPositions", "State_Id", "dbo.States");
            DropForeignKey("dbo.CustomerOrderPositions", "ProductId", "dbo.Products");
            DropForeignKey("dbo.CustomerOrders", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.CustomerOrederDeliveryAddress", "CustomerOrder_Id", "dbo.CustomerOrders");
            DropForeignKey("dbo.CustomerOrderPositions", "CustomerOrder_Id", "dbo.CustomerOrders");
            DropForeignKey("dbo.CustomerOrders", "CustomerId", "dbo.Customers");
            DropForeignKey("dbo.CustomerResidentalAddress", "Id", "dbo.Customers");
            DropForeignKey("dbo.CustomerOrders", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.CustomerOrders", "CourierId", "dbo.Couriers");
            DropForeignKey("dbo.WarehouseOrders", "ExternalSupplayerId", "dbo.ExternalSuppliers");
            DropForeignKey("dbo.ExternalSupplierPhones", "ExternalSupplier_Id", "dbo.ExternalSuppliers");
            DropForeignKey("dbo.ExternalSupplierEmailAddresses", "Id", "dbo.ExternalSuppliers");
            DropForeignKey("dbo.ExternalSuppliersAddresses", "Id", "dbo.ExternalSuppliers");
            DropForeignKey("dbo.WarehouseOrders", "CreatorId", "dbo.Users");
            DropForeignKey("dbo.Messages", "Sender_Id", "dbo.Users");
            DropForeignKey("dbo.Messages", "Receiver_Id", "dbo.Users");
            DropIndex("dbo.CouriersPhones", new[] { "Courier_Id" });
            DropIndex("dbo.CouriersPhones", new[] { "Id" });
            DropIndex("dbo.UsersPhones", new[] { "User_Id" });
            DropIndex("dbo.UsersPhones", new[] { "Id" });
            DropIndex("dbo.UsersEmailAddresses", new[] { "User_Id" });
            DropIndex("dbo.UsersEmailAddresses", new[] { "Id" });
            DropIndex("dbo.Users", new[] { "Id" });
            DropIndex("dbo.WarehouseAddress", new[] { "Warehouse_Id" });
            DropIndex("dbo.WarehouseAddress", new[] { "Id" });
            DropIndex("dbo.ExternalSuppliers", new[] { "Id" });
            DropIndex("dbo.Couriers", new[] { "Id" });
            DropIndex("dbo.States", new[] { "WarehouseId" });
            DropIndex("dbo.States", new[] { "ProductId" });
            DropIndex("dbo.CustomerOrederDeliveryAddress", new[] { "CustomerOrder_Id" });
            DropIndex("dbo.CustomerResidentalAddress", new[] { "Id" });
            DropIndex("dbo.CustomerOrders", new[] { "CourierId" });
            DropIndex("dbo.CustomerOrders", new[] { "CustomerId" });
            DropIndex("dbo.CustomerOrders", new[] { "CreatorId" });
            DropIndex("dbo.CustomerOrders", new[] { "WarehouseId" });
            DropIndex("dbo.CustomerOrderPositions", new[] { "State_Id" });
            DropIndex("dbo.CustomerOrderPositions", new[] { "CustomerOrder_Id" });
            DropIndex("dbo.CustomerOrderPositions", new[] { "ProductId" });
            DropIndex("dbo.WarehouseOrderPositions", new[] { "WarehouseOrder_Id" });
            DropIndex("dbo.WarehouseOrderPositions", new[] { "ProductId" });
            DropIndex("dbo.ExternalSupplierPhones", new[] { "ExternalSupplier_Id" });
            DropIndex("dbo.ExternalSupplierEmailAddresses", new[] { "Id" });
            DropIndex("dbo.ExternalSuppliersAddresses", new[] { "Id" });
            DropIndex("dbo.WarehouseOrders", new[] { "ExternalSupplayerId" });
            DropIndex("dbo.WarehouseOrders", new[] { "UserReceivedOrderId" });
            DropIndex("dbo.WarehouseOrders", new[] { "CreatorId" });
            DropIndex("dbo.WarehouseOrders", new[] { "WarehouseId" });
            DropIndex("dbo.Messages", new[] { "Sender_Id" });
            DropIndex("dbo.Messages", new[] { "Receiver_Id" });
            DropIndex("dbo.People", "IX_FirstName");
            DropIndex("dbo.AccessUsersToWarehouses", new[] { "Warehouse_Id" });
            DropIndex("dbo.AccessUsersToWarehouses", new[] { "User_Id" });
            DropIndex("dbo.AccessUsersToWarehouses", new[] { "Administrator_Id" });
            DropTable("dbo.CouriersPhones");
            DropTable("dbo.UsersPhones");
            DropTable("dbo.UsersEmailAddresses");
            DropTable("dbo.Users");
            DropTable("dbo.WarehouseAddress");
            DropTable("dbo.ExternalSuppliers");
            DropTable("dbo.Couriers");
            DropTable("dbo.States");
            DropTable("dbo.Addresses");
            DropTable("dbo.Warehouses");
            DropTable("dbo.CustomerOrederDeliveryAddress");
            DropTable("dbo.CustomerResidentalAddress");
            DropTable("dbo.Customers");
            DropTable("dbo.CustomerOrders");
            DropTable("dbo.CustomerOrderPositions");
            DropTable("dbo.Products");
            DropTable("dbo.WarehouseOrderPositions");
            DropTable("dbo.ExternalSupplierPhones");
            DropTable("dbo.ExternalSupplierEmailAddresses");
            DropTable("dbo.ExternalSuppliersAddresses");
            DropTable("dbo.WarehouseOrders");
            DropTable("dbo.Phones");
            DropTable("dbo.Messages");
            DropTable("dbo.EmailAddresses");
            DropTable("dbo.People");
            DropTable("dbo.AccessUsersToWarehouses");
        }
    }
}
