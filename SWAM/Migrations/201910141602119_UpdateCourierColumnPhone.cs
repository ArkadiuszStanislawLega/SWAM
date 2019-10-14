namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateCourierColumnPhone : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Couriers", "Id", "dbo.CouriersPhones");
            AddColumn("dbo.Couriers", "Phone", c => c.String());
            AddColumn("dbo.CouriersPhones", "Courier_Id", c => c.Int());
            CreateIndex("dbo.CouriersPhones", "Courier_Id");
            AddForeignKey("dbo.CouriersPhones", "Courier_Id", "dbo.Couriers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CouriersPhones", "Courier_Id", "dbo.Couriers");
            DropIndex("dbo.CouriersPhones", new[] { "Courier_Id" });
            DropColumn("dbo.CouriersPhones", "Courier_Id");
            DropColumn("dbo.Couriers", "Phone");
            AddForeignKey("dbo.Couriers", "Id", "dbo.CouriersPhones", "Id");
        }
    }
}
