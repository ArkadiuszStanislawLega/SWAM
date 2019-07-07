namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixError : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "Warehouse_Id", "dbo.Warehouses");
            DropForeignKey("dbo.AccessUsersToWarehouses", "UserId", "dbo.Users");
            DropForeignKey("dbo.AccessUsersToWarehouses", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.AccessUsersToWarehouses", "User_Id", "dbo.Users");
            DropForeignKey("dbo.AccessUsersToWarehouses", "Administrator_Id", "dbo.Users");
            DropIndex("dbo.AccessUsersToWarehouses", new[] { "UserId" });
            DropIndex("dbo.AccessUsersToWarehouses", new[] { "User_Id" });
            DropIndex(table: "dbo.AccessUsersToWarehouses", name: "IX_AdministratorId");
            DropIndex("dbo.AccessUsersToWarehouses", new[] { "Administrator_Id" });
            DropIndex("dbo.Users", new[] { "Warehouse_Id" });
            AlterColumn("dbo.AccessUsersToWarehouses", "AdministratorId", c => c.Int(nullable: false));
            CreateIndex("dbo.AccessUsersToWarehouses", "AdministratorId");
            AlterColumn("dbo.AccessUsersToWarehouses", "UserId", c => c.Int(nullable: false));
            CreateIndex("dbo.AccessUsersToWarehouses", "UserId");
            AddForeignKey("dbo.AccessUsersToWarehouses", "UserId", "dbo.Users", "Id", cascadeDelete: false);
            AddForeignKey("dbo.AccessUsersToWarehouses", "AdministratorId", "dbo.Users", "Id", cascadeDelete: false);
            AddForeignKey("dbo.AccessUsersToWarehouses", "WarehouseId", "dbo.Warehouses", "Id", cascadeDelete: true);
            DropColumn("dbo.Users", "Warehouse_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "Warehouse_Id", c => c.Int());
            DropForeignKey("dbo.AccessUsersToWarehouses", "WarehouseId", "dbo.Warehouses");
            DropForeignKey("dbo.AccessUsersToWarehouses", "AdministratorId", "dbo.Users");
            DropIndex("dbo.AccessUsersToWarehouses", new[] { "AdministratorId" });
            AlterColumn("dbo.AccessUsersToWarehouses", "AdministratorId", c => c.Int());
            RenameIndex(table: "dbo.AccessUsersToWarehouses", name: "IX_UserId", newName: "IX_AdministratorId");
            RenameColumn(table: "dbo.AccessUsersToWarehouses", name: "UserId", newName: "__mig_tmp__1");
            RenameColumn(table: "dbo.AccessUsersToWarehouses", name: "AdministratorId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.AccessUsersToWarehouses", name: "AdministratorId", newName: "User_Id");
            RenameColumn(table: "dbo.AccessUsersToWarehouses", name: "__mig_tmp__1", newName: "AdministratorId");
            RenameColumn(table: "dbo.AccessUsersToWarehouses", name: "__mig_tmp__0", newName: "UserId");
            CreateIndex("dbo.Users", "Warehouse_Id");
            CreateIndex("dbo.AccessUsersToWarehouses", "User_Id");
            CreateIndex("dbo.AccessUsersToWarehouses", "UserId");
            AddForeignKey("dbo.AccessUsersToWarehouses", "User_Id", "dbo.Users", "Id");
            AddForeignKey("dbo.AccessUsersToWarehouses", "WarehouseId", "dbo.Warehouses", "Id");
            AddForeignKey("dbo.AccessUsersToWarehouses", "UserId", "dbo.Users", "Id");
            AddForeignKey("dbo.Users", "Warehouse_Id", "dbo.Warehouses", "Id");
        }
    }
}
