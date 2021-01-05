namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAddressToLpO : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LpoLocations",
                c => new
                    {
                        LpoLocationId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.LpoLocationId);
            
            AddColumn("dbo.Lpoes", "LpoLocationId", c => c.Int());
            CreateIndex("dbo.Lpoes", "LpoLocationId");
            AddForeignKey("dbo.Lpoes", "LpoLocationId", "dbo.LpoLocations", "LpoLocationId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lpoes", "LpoLocationId", "dbo.LpoLocations");
            DropIndex("dbo.Lpoes", new[] { "LpoLocationId" });
            DropColumn("dbo.Lpoes", "LpoLocationId");
            DropTable("dbo.LpoLocations");
        }
    }
}
