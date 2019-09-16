namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class MessageModelAddedFlagsAboutDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "IsDeletedBySender", c => c.Boolean(nullable: false));
            AddColumn("dbo.Messages", "IsDeletedByReceiver", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "IsDeletedByReceiver");
            DropColumn("dbo.Messages", "IsDeletedBySender");
        }
    }
}
