namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteDataTime2FromMessageModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "PostDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Messages", "DateOfReading", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "DateOfReading", c => c.DateTime(precision: 0, storeType: "datetime2"));
            AlterColumn("dbo.Messages", "PostDate", c => c.DateTime(nullable: false, precision: 0, storeType: "datetime2"));
        }
    }
}
