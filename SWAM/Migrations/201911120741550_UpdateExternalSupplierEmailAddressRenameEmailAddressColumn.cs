namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateExternalSupplierEmailAddressRenameEmailAddressColumn : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ExternalSupplierEmailAddresses", "EmailAddress", c => c.String());
            DropColumn("dbo.ExternalSupplierEmailAddresses", "AddressEmail");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ExternalSupplierEmailAddresses", "AddressEmail", c => c.String());
            DropColumn("dbo.ExternalSupplierEmailAddresses", "EmailAddress");
        }
    }
}
