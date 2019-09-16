namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AccessUserTowarehousesDebugAdministratorIdProperty : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AccessUsersToWarehouses", "UserId", "dbo.Users");
            DropIndex("dbo.AccessUsersToWarehouses", new[] { "Administrator_Id" });
            RenameColumn(table: "dbo.AccessUsersToWarehouses", name: "Administrator_Id", newName: "AdministratorId");
            AlterColumn("dbo.AccessUsersToWarehouses", "AdministratorId", c => c.Int(nullable: false));
            CreateIndex("dbo.AccessUsersToWarehouses", "AdministratorId");
            AddForeignKey("dbo.AccessUsersToWarehouses", "UserId", "dbo.Users", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AccessUsersToWarehouses", "UserId", "dbo.Users");
            DropIndex("dbo.AccessUsersToWarehouses", new[] { "AdministratorId" });
            AlterColumn("dbo.AccessUsersToWarehouses", "AdministratorId", c => c.Int());
            RenameColumn(table: "dbo.AccessUsersToWarehouses", name: "AdministratorId", newName: "Administrator_Id");
            CreateIndex("dbo.AccessUsersToWarehouses", "Administrator_Id");
            AddForeignKey("dbo.AccessUsersToWarehouses", "UserId", "dbo.Users", "Id", cascadeDelete: true);
        }
    }
}
