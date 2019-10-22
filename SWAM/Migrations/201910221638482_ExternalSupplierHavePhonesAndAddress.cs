namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ExternalSupplierHavePhonesAndAddress : DbMigration
    {
        public override void Up()
        {
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
                "dbo.ExternalSuppliersPhones",
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExternalSuppliersPhones", "ExternalSupplier_Id", "dbo.ExternalSuppliers");
            DropForeignKey("dbo.ExternalSuppliersAddresses", "Id", "dbo.ExternalSuppliers");
            DropIndex("dbo.ExternalSuppliersPhones", new[] { "ExternalSupplier_Id" });
            DropIndex("dbo.ExternalSuppliersAddresses", new[] { "Id" });
            DropTable("dbo.ExternalSuppliersPhones");
            DropTable("dbo.ExternalSuppliersAddresses");
        }
    }
}
