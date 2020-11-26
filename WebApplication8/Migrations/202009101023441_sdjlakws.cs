namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sdjlakws : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.suppliers", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.suppliers", "Name", c => c.String());
        }
    }
}
