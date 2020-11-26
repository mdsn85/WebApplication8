namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fhf1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CuttingSheetSequenses",
                c => new
                    {
                        CuttingSheetSequenseId = c.Int(nullable: false, identity: true),
                        Sequense = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.CuttingSheetSequenseId);
            
            AddColumn("dbo.CuttingSheets", "code", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.CuttingSheets", "code");
            DropTable("dbo.CuttingSheetSequenses");
        }
    }
}
