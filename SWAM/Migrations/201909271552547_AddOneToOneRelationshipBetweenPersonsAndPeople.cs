namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOneToOneRelationshipBetweenPhonesAndPeople : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Phones");
            AlterColumn("dbo.Phones", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Phones", "Id");
            CreateIndex("dbo.Phones", "Id");
            AddForeignKey("dbo.Phones", "Id", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Phones", "Id", "dbo.People");
            DropIndex("dbo.Phones", new[] { "Id" });
            DropPrimaryKey("dbo.Phones");
            AlterColumn("dbo.Phones", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Phones", "Id");
        }
    }
}
