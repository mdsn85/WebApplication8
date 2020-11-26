namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ggs : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProjectStatus",
                c => new
                    {
                        ProjectStatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ProjectStatusId);
            
            AddColumn("dbo.Projects", "ProjectStatusId", c => c.Int(nullable: true));
            CreateIndex("dbo.Projects", "ProjectStatusId");
            AddForeignKey("dbo.Projects", "ProjectStatusId", "dbo.ProjectStatus", "ProjectStatusId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "ProjectStatusId", "dbo.ProjectStatus");
            DropIndex("dbo.Projects", new[] { "ProjectStatusId" });
            DropColumn("dbo.Projects", "ProjectStatusId");
            DropTable("dbo.ProjectStatus");
        }
    }
}
