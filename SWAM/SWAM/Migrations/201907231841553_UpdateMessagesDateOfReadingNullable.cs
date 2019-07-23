namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMessagesDateOfReadingNullable : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Messages", "DateOfReading", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Messages", "DateOfReading", c => c.DateTime(nullable: false));
        }
    }
}
