namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationshipBetweenExternalSupplierEmialAdressesAndExternalSuppliersTables : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExternalSupplierEmailAddresses", "ExternalSupplierId", c => c.Int(nullable: false));
            AddForeignKey("dbo.ExternalSupplierEmailAddresses", "Id", "dbo.ExternalSuppliers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ExternalSupplierEmailAddresses", "Id", "dbo.ExternalSuppliers");
            DropColumn("dbo.ExternalSupplierEmailAddresses", "ExternalSupplierId");
        }
    }
}
