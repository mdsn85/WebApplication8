namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class renameNotificationCatTable : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.NotificationCatCatUsers", "NotificationCategoryId", "dbo.NotificationCategories");
            DropForeignKey("dbo.NotificationCatCatUsers", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.NotificationCatCatUsers", new[] { "NotificationCategoryId" });
            DropIndex("dbo.NotificationCatCatUsers", new[] { "UserId" });
            CreateTable(
                "dbo.NotificationCatUsers",
                c => new
                    {
                        NotificationCatUserId = c.Int(nullable: false, identity: true),
                        NotificationCategoryId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.NotificationCatUserId)
                .ForeignKey("dbo.NotificationCategories", t => t.NotificationCategoryId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.NotificationCategoryId)
                .Index(t => t.UserId);
            
            DropTable("dbo.NotificationCatCatUsers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.NotificationCatCatUsers",
                c => new
                    {
                        NotificationCatCatUserId = c.Int(nullable: false, identity: true),
                        NotificationCategoryId = c.Int(nullable: false),
                        UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.NotificationCatCatUserId);
            
            DropForeignKey("dbo.NotificationCatUsers", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.NotificationCatUsers", "NotificationCategoryId", "dbo.NotificationCategories");
            DropIndex("dbo.NotificationCatUsers", new[] { "UserId" });
            DropIndex("dbo.NotificationCatUsers", new[] { "NotificationCategoryId" });
            DropTable("dbo.NotificationCatUsers");
            CreateIndex("dbo.NotificationCatCatUsers", "UserId");
            CreateIndex("dbo.NotificationCatCatUsers", "NotificationCategoryId");
            AddForeignKey("dbo.NotificationCatCatUsers", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.NotificationCatCatUsers", "NotificationCategoryId", "dbo.NotificationCategories", "NotificationCategoryId", cascadeDelete: true);
        }
    }
}
