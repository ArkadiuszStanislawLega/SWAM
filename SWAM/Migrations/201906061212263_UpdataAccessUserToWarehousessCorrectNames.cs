namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdataAccessUserToWarehousessCorrectNames : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccessUsersToWarehouses", "DateOfGrantingAccess", c => c.DateTime(nullable: false));
            DropColumn("dbo.AccessUsersToWarehouses", "DateOfGrntingAcces");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccessUsersToWarehouses", "DateOfGrntingAcces", c => c.DateTime(nullable: false));
            DropColumn("dbo.AccessUsersToWarehouses", "DateOfGrantingAccess");
        }
    }
}
