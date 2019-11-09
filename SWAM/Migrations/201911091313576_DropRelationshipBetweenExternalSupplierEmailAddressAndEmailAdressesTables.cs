namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DropRelationshipBetweenExternalSupplierEmailAddressAndEmailAdressesTables : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ExternalSupplierEmailAddresses", "Id", "dbo.EmailAddresses");
            AddColumn("dbo.ExternalSupplierEmailAddresses", "AddressEmail", c => c.String());
            AddColumn("dbo.ExternalSupplierEmailAddresses", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ExternalSupplierEmailAddresses", "Note");
            DropColumn("dbo.ExternalSupplierEmailAddresses", "AddressEmail");
            AddForeignKey("dbo.ExternalSupplierEmailAddresses", "Id", "dbo.EmailAddresses", "Id");
        }
    }
}
