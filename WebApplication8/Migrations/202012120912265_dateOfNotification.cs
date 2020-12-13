namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dateOfNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notification1", "NotificationDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Notification1", "NotificationDate");
        }
    }
}
