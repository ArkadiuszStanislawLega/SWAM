namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewTableAccessUsersToWarehouses : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "ResidentAddress_Id", "dbo.Addresses");
            DropIndex("dbo.Users", new[] { "ResidentAddress_Id" });
            CreateTable(
                "dbo.AccessUsersToWarehouses",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        WarehouseId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Users", "ResidentAddress_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Users", "ResidentAddress_Id", c => c.Int());
            DropTable("dbo.AccessUsersToWarehouses");
            CreateIndex("dbo.Users", "ResidentAddress_Id");
            AddForeignKey("dbo.Users", "ResidentAddress_Id", "dbo.Addresses", "Id");
        }
    }
}
