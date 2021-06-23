namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lead_lastmeeting_Optional : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Leads", "LastMeetingDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Leads", "LastMeetingDate", c => c.DateTime(nullable: false));
        }
    }
}
