namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatePhoneAddNote : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Phones", "Note", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Phones", "Note");
        }
    }
}
