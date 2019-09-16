namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class accessToWarehouseHaveRequiredWarehouse : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccessUsersToWarehouses", "WarehouseId", "dbo.Warehouses");
            RenameColumn(table: "dbo.AccessUsersToWarehouses", name: "AdministratorId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.AccessUsersToWarehouses", name: "UserId", newName: "AdministratorId");
            RenameColumn(table: "dbo.AccessUsersToWarehouses", name: "__mig_tmp__0", newName: "UserId");
            RenameIndex(table: "dbo.AccessUsersToWarehouses", name: "IX_AdministratorId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.AccessUsersToWarehouses", name: "IX_UserId", newName: "IX_AdministratorId");
            RenameIndex(table: "dbo.AccessUsersToWarehouses", name: "__mig_tmp__0", newName: "IX_UserId");
            AddForeignKey("dbo.AccessUsersToWarehouses", "WarehouseId", "dbo.Warehouses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccessUsersToWarehouses", "WarehouseId", "dbo.Warehouses");
            RenameIndex(table: "dbo.AccessUsersToWarehouses", name: "IX_UserId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.AccessUsersToWarehouses", name: "IX_AdministratorId", newName: "IX_UserId");
            RenameIndex(table: "dbo.AccessUsersToWarehouses", name: "__mig_tmp__0", newName: "IX_AdministratorId");
            RenameColumn(table: "dbo.AccessUsersToWarehouses", name: "UserId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.AccessUsersToWarehouses", name: "AdministratorId", newName: "UserId");
            RenameColumn(table: "dbo.AccessUsersToWarehouses", name: "__mig_tmp__0", newName: "AdministratorId");
            AddForeignKey("dbo.AccessUsersToWarehouses", "WarehouseId", "dbo.Warehouses", "Id", cascadeDelete: true);
        }
    }
}
