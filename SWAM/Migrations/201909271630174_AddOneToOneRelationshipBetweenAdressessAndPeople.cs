namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOneToOneRelationshipBetweenAdressessAndPeople : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Addresses");
            AlterColumn("dbo.Addresses", "Id", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.Addresses", "Id");
            CreateIndex("dbo.Addresses", "Id");
            AddForeignKey("dbo.Addresses", "Id", "dbo.People", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Addresses", "Id", "dbo.People");
            DropIndex("dbo.Addresses", new[] { "Id" });
            DropPrimaryKey("dbo.Addresses");
            AlterColumn("dbo.Addresses", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.Addresses", "Id");
        }
    }
}
