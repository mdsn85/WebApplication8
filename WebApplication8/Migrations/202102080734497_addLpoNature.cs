namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLpoNature : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LpoNatures",
                c => new
                    {
                        LpoNatureId = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 450),
                    })
                .PrimaryKey(t => t.LpoNatureId);
            
            AddColumn("dbo.Lpoes", "LpoNatureId", c => c.Int());
            CreateIndex("dbo.Lpoes", "LpoNatureId");
            AddForeignKey("dbo.Lpoes", "LpoNatureId", "dbo.LpoNatures", "LpoNatureId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lpoes", "LpoNatureId", "dbo.LpoNatures");
            DropIndex("dbo.Lpoes", new[] { "LpoNatureId" });
            DropColumn("dbo.Lpoes", "LpoNatureId");
            DropTable("dbo.LpoNatures");
        }
    }
}
