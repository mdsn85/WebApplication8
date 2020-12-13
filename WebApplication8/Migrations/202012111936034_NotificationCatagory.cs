namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NotificationCatagory : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.NotificationCategories",
                c => new
                    {
                        NotificationCategoryId = c.Int(nullable: false, identity: true),
                        Title = c.String(maxLength: 150),
                        Details = c.String(maxLength: 250),
                        DetailsURL = c.String(maxLength: 250),
                        IsRead = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        IsReminder = c.Boolean(),
                        NotificationType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.NotificationCategoryId);
            
            CreateTable(
                "dbo.NotificationCatCatUsers",
                c => new
                    {
                        NotificationCatCatUserId = c.Int(nullable: false, identity: true),
                        NotificationCategoryId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.NotificationCatCatUserId)
                .ForeignKey("dbo.NotificationCategories", t => t.NotificationCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.NotificationCategoryId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Notification1",
                c => new
                    {
                        Notification1Id = c.Int(nullable: false, identity: true),
                        ObjectId = c.Int(),
                        NotificationCategoryId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Notification1Id)
                .ForeignKey("dbo.NotificationCategories", t => t.NotificationCategoryId, cascadeDelete: true)
                .Index(t => t.NotificationCategoryId);
            
            DropTable("dbo.Notifications");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Notifications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(),
                        Details = c.String(),
                        Title = c.String(),
                        DetailsURL = c.String(),
                        SentTo = c.String(),
                        Date = c.DateTime(),
                        IsRead = c.Boolean(),
                        IsDeleted = c.Boolean(),
                        IsReminder = c.Boolean(),
                        Code = c.String(),
                        NotificationType = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Notification1", "NotificationCategoryId", "dbo.NotificationCategories");
            DropForeignKey("dbo.NotificationCatCatUsers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.NotificationCatCatUsers", "NotificationCategoryId", "dbo.NotificationCategories");
            DropIndex("dbo.Notification1", new[] { "NotificationCategoryId" });
            DropIndex("dbo.NotificationCatCatUsers", new[] { "UserId" });
            DropIndex("dbo.NotificationCatCatUsers", new[] { "NotificationCategoryId" });
            DropTable("dbo.Notification1");
            DropTable("dbo.NotificationCatCatUsers");
            DropTable("dbo.NotificationCategories");
        }
    }
}
