namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addWarantyAndAreaToProject : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Areas",
                c => new
                    {
                        AreaId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 50),
                    })
                .PrimaryKey(t => t.AreaId);
            
            AddColumn("dbo.Projects", "Warranty", c => c.DateTime(nullable: false));
            AddColumn("dbo.Projects", "AreaId", c => c.Int());
            CreateIndex("dbo.Projects", "AreaId");
            AddForeignKey("dbo.Projects", "AreaId", "dbo.Areas", "AreaId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "AreaId", "dbo.Areas");
            DropIndex("dbo.Projects", new[] { "AreaId" });
            DropColumn("dbo.Projects", "AreaId");
            DropColumn("dbo.Projects", "Warranty");
            DropTable("dbo.Areas");
        }
    }
}
