namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateNotification : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Notification1", "IsRead", c => c.Boolean());
            AddColumn("dbo.Notification1", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.Notification1", "IsReminder", c => c.Boolean());
            DropColumn("dbo.NotificationCategories", "IsRead");
            DropColumn("dbo.NotificationCategories", "IsDeleted");
            DropColumn("dbo.NotificationCategories", "IsReminder");
        }
        
        public override void Down()
        {
            AddColumn("dbo.NotificationCategories", "IsReminder", c => c.Boolean());
            AddColumn("dbo.NotificationCategories", "IsDeleted", c => c.Boolean());
            AddColumn("dbo.NotificationCategories", "IsRead", c => c.Boolean());
            DropColumn("dbo.Notification1", "IsReminder");
            DropColumn("dbo.Notification1", "IsDeleted");
            DropColumn("dbo.Notification1", "IsRead");
        }
    }
}
