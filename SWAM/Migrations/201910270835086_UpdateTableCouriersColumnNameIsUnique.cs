namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateTableCouriersColumnNameIsUnique : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "Name", c => c.String(nullable: false, maxLength: 60));
            CreateIndex("dbo.People", "Name", unique: true, name: "IX_FirstName");
        }
        
        public override void Down()
        {
            DropIndex("dbo.People", "IX_FirstName");
            AlterColumn("dbo.People", "Name", c => c.String());
        }
    }
}
