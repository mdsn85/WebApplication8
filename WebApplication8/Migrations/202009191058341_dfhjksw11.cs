namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw11 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project2", "ProjectPaymentTermId", c => c.Int(nullable: false));
            CreateIndex("dbo.Project2", "ProjectPaymentTermId");
            AddForeignKey("dbo.Project2", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms", "ProjectPaymentTermId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project2", "ProjectPaymentTermId", "dbo.ProjectPaymentTerms");
            DropIndex("dbo.Project2", new[] { "ProjectPaymentTermId" });
            DropColumn("dbo.Project2", "ProjectPaymentTermId");
        }
    }
}
