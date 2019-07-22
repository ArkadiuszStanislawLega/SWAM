namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMessageModelAddTitle : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Messages", "TitleOfMessage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Messages", "TitleOfMessage");
        }
    }
}
