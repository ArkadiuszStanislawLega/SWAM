namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EnableCascadeDeleteForCustomerOrderPositions : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.CustomerOrderPositions", "CustomerOrder_Id", "dbo.CustomerOrders");
            DropIndex("dbo.CustomerOrderPositions", new[] { "CustomerOrder_Id" });
            AlterColumn("dbo.CustomerOrderPositions", "CustomerOrder_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.CustomerOrderPositions", "CustomerOrder_Id");
            AddForeignKey("dbo.CustomerOrderPositions", "CustomerOrder_Id", "dbo.CustomerOrders", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CustomerOrderPositions", "CustomerOrder_Id", "dbo.CustomerOrders");
            DropIndex("dbo.CustomerOrderPositions", new[] { "CustomerOrder_Id" });
            AlterColumn("dbo.CustomerOrderPositions", "CustomerOrder_Id", c => c.Int());
            CreateIndex("dbo.CustomerOrderPositions", "CustomerOrder_Id");
            AddForeignKey("dbo.CustomerOrderPositions", "CustomerOrder_Id", "dbo.CustomerOrders", "Id");
        }
    }
}
