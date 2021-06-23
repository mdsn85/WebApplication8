namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class leadsLastUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leads", "WhoLastUpdate", c => c.String());
            AddColumn("dbo.Leads", "DateOfUpdate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leads", "DateOfUpdate");
            DropColumn("dbo.Leads", "WhoLastUpdate");
        }
    }
}
