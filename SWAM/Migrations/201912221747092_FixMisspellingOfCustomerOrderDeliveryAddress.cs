namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixMisspellingOfCustomerOrderDeliveryAddress : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CustomerOrederDeliveryAddress", newName: "CustomerOrderDeliveryAddress");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CustomerOrderDeliveryAddress", newName: "CustomerOrederDeliveryAddress");
        }
    }
}
