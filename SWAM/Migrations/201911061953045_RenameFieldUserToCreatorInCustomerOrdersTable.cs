namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameFieldUserToCreatorInCustomerOrdersTable : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.CustomerOrders", name: "UserId", newName: "CreatorId");
            RenameIndex(table: "dbo.CustomerOrders", name: "IX_UserId", newName: "IX_CreatorId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.CustomerOrders", name: "IX_CreatorId", newName: "IX_UserId");
            RenameColumn(table: "dbo.CustomerOrders", name: "CreatorId", newName: "UserId");
        }
    }
}
