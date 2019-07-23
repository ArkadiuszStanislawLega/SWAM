namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMessagesDatesAreDateTime2Type : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "PostDate", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
            AlterColumn("dbo.Messages", "DateOfReading", c => c.DateTime(precision: 0, storeType: "datetime2"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "DateOfReading", c => c.DateTime());
            AlterColumn("dbo.Messages", "PostDate", c => c.DateTime(nullable: false));
        }
    }
}
