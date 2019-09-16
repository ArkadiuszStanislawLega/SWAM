namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ManyToManyUsersAccesses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccessUsersToWarehouses", "UserId", "dbo.Users");
            AddColumn("dbo.AccessUsersToWarehouses", "User_Id", c => c.Int());
            AddColumn("dbo.AccessUsersToWarehouses", "Administrator_Id", c => c.Int());
            CreateIndex("dbo.AccessUsersToWarehouses", "WarehouseId");
            CreateIndex("dbo.AccessUsersToWarehouses", "User_Id");
            CreateIndex("dbo.AccessUsersToWarehouses", "Administrator_Id");
            AddForeignKey("dbo.AccessUsersToWarehouses", "Administrator_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.AccessUsersToWarehouses", "UserId", "dbo.Users", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AccessUsersToWarehouses", "WarehouseId", "dbo.Warehouses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.AccessUsersToWarehouses", "User_Id", "dbo.Users", "Id");
            DropColumn("dbo.AccessUsersToWarehouses", "WhoGaveAccessUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AccessUsersToWarehouses", "WhoGaveAccessUserId", c => c.Int(nullable: false));
            DropForeignKey("dbo.AccessUsersToWarehouses", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AccessUsersToWarehouses", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.AccessUsersToWarehouses", "UserId", "dbo.Users");
            DropForeignKey("dbo.AccessUsersToWarehouses", "Administrator_Id", "dbo.Users");
            DropIndex("dbo.AccessUsersToWarehouses", new[] { "Administrator_Id" });
            DropIndex("dbo.AccessUsersToWarehouses", new[] { "User_Id" });
            DropIndex("dbo.AccessUsersToWarehouses", new[] { "WarehouseId" });
            DropColumn("dbo.AccessUsersToWarehouses", "Administrator_Id");
            DropColumn("dbo.AccessUsersToWarehouses", "User_Id");
            AddForeignKey("dbo.AccessUsersToWarehouses", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
