namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class leadaddStamp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Leads", "StampDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Leads", "StampDate");
        }
    }
}
