namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fjf : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LpoStatus",
                c => new
                    {
                        LpoStatusId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.LpoStatusId);
            
            AddColumn("dbo.Lpoes", "LpoStatusId", c => c.Int());
            CreateIndex("dbo.Lpoes", "LpoStatusId");
            AddForeignKey("dbo.Lpoes", "LpoStatusId", "dbo.LpoStatus", "LpoStatusId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lpoes", "LpoStatusId", "dbo.LpoStatus");
            DropIndex("dbo.Lpoes", new[] { "LpoStatusId" });
            DropColumn("dbo.Lpoes", "LpoStatusId");
            DropTable("dbo.LpoStatus");
        }
    }
}
