namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAddressToLpOSize : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.LpoLocations", "Name", c => c.String(maxLength: 450));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.LpoLocations", "Name", c => c.String());
        }
    }
}
