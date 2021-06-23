namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lead1 : DbMigration
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
