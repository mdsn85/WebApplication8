namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileUploadToCuttingSheet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CuttingSheetFiles",
                c => new
                    {
                        CuttingSheetFileId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Path = c.String(),
                        ValidUntil = c.DateTime(),
                        CuttingSheetId = c.Int(),
                    })
                .PrimaryKey(t => t.CuttingSheetFileId)
                .ForeignKey("dbo.CuttingSheets", t => t.CuttingSheetId)
                .Index(t => t.CuttingSheetId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CuttingSheetFiles", "CuttingSheetId", "dbo.CuttingSheets");
            DropIndex("dbo.CuttingSheetFiles", new[] { "CuttingSheetId" });
            DropTable("dbo.CuttingSheetFiles");
        }
    }
}
