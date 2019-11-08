namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExternalSupplierEmailAddressTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ExternalSupplierEmailAddresses",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.EmailAddresses", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExternalSupplierEmailAddresses", "Id", "dbo.EmailAddresses");
            DropIndex("dbo.ExternalSupplierEmailAddresses", new[] { "Id" });
            DropTable("dbo.ExternalSupplierEmailAddresses");
        }
    }
}
