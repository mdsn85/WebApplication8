namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ggslks : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ContractFiles", "ProjectId", "dbo.Projects");
            DropIndex("dbo.ContractFiles", new[] { "ProjectId" });
            DropTable("dbo.ContractFiles");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.ContractFileId);
            
            CreateIndex("dbo.ContractFiles", "ProjectId");
            AddForeignKey("dbo.ContractFiles", "ProjectId", "dbo.Projects", "ProjectId");
        }
    }
}
