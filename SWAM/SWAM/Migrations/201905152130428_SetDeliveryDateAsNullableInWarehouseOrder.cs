namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SetDeliveryDateAsNullableInWarehouseOrder : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.WarehouseOrders", "DeliveryDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.WarehouseOrders", "DeliveryDate", c => c.DateTime(nullable: false));
        }
    }
}
