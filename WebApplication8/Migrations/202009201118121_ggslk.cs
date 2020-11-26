namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ggslk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ContractFiles",
                c => new
                    {
                        ContractFileId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        ValidUntil = c.DateTime(nullable: false),
                        ProjectId = c.Int(),
                    })
                .PrimaryKey(t => t.ContractFileId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
            CreateTable(
                "dbo.ProjectFiles",
                c => new
                    {
                        ProjectFileId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        ValidUntil = c.DateTime(nullable: false),
                        ProjectId = c.Int(),
                    })
                .PrimaryKey(t => t.ProjectFileId)
                .ForeignKey("dbo.Projects", t => t.ProjectId)
                .Index(t => t.ProjectId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProjectFiles", "ProjectId", "dbo.Projects");
            DropForeignKey("dbo.ContractFiles", "ProjectId", "dbo.Projects");
            DropIndex("dbo.ProjectFiles", new[] { "ProjectId" });
            DropIndex("dbo.ContractFiles", new[] { "ProjectId" });
            DropTable("dbo.ProjectFiles");
            DropTable("dbo.ContractFiles");
        }
    }
}
