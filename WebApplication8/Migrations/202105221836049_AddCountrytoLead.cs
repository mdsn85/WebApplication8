namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountrytoLead : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        CountryId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CountryId);
            
            AddColumn("dbo.Leads", "CountryId", c => c.Int());
            CreateIndex("dbo.Leads", "CountryId");
            AddForeignKey("dbo.Leads", "CountryId", "dbo.Countries", "CountryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Leads", "CountryId", "dbo.Countries");
            DropIndex("dbo.Leads", new[] { "CountryId" });
            DropColumn("dbo.Leads", "CountryId");
            DropTable("dbo.Countries");
        }
    }
}
