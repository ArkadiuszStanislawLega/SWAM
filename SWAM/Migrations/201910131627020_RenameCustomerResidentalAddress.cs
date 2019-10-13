namespace SWAM.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RenameCustomerResidentalAddress : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.CusomtersResidentalAddresses", newName: "CustomerResidentalAddress");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.CustomerResidentalAddress", newName: "CusomtersResidentalAddresses");
        }
    }
}
