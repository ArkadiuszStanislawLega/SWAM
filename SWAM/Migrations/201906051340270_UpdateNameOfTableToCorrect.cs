namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateNameOfTableToCorrect : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ints", newName: "Addresses");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Addresses", newName: "ints");
        }
    }
}
