namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hfh : DbMigration
    {
        public override void Up()
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Notifications");
        }
    }
}
