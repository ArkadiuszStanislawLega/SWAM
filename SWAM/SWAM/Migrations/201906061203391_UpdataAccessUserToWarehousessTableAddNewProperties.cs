namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdataAccessUserToWarehousessTableAddNewProperties : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AccessUsersToWarehouses", "WhoGaveAccessUserId", c => c.Int(nullable: false));
            AddColumn("dbo.AccessUsersToWarehouses", "TypeOfAccess", c => c.Int(nullable: false));
            AddColumn("dbo.AccessUsersToWarehouses", "DateOfGrntingAcces", c => c.DateTime(nullable: false));
            AddColumn("dbo.AccessUsersToWarehouses", "DateOfExpiredAcces", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AccessUsersToWarehouses", "DateOfExpiredAcces");
            DropColumn("dbo.AccessUsersToWarehouses", "DateOfGrntingAcces");
            DropColumn("dbo.AccessUsersToWarehouses", "TypeOfAccess");
            DropColumn("dbo.AccessUsersToWarehouses", "WhoGaveAccessUserId");
        }
    }
}
