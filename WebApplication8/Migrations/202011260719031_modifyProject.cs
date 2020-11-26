namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyProject : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Projects", "QuotationRef", c => c.String(maxLength: 20));
            AddColumn("dbo.Projects", "QuotationAgreementApprovedby", c => c.String(maxLength: 50));
            AlterColumn("dbo.Projects", "Name", c => c.String(maxLength: 150));
            AlterColumn("dbo.Projects", "Code", c => c.String(maxLength: 20));
            AlterColumn("dbo.Projects", "DeductionReason", c => c.String(maxLength: 350));
            AlterColumn("dbo.Projects", "Description", c => c.String(maxLength: 350));
            AlterColumn("dbo.Projects", "UserCreate", c => c.String(maxLength: 20));
            AlterColumn("dbo.Projects", "UserLastUpdate", c => c.String(maxLength: 20));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Projects", "UserLastUpdate", c => c.String());
            AlterColumn("dbo.Projects", "UserCreate", c => c.String());
            AlterColumn("dbo.Projects", "Description", c => c.String());
            AlterColumn("dbo.Projects", "DeductionReason", c => c.String());
            AlterColumn("dbo.Projects", "Code", c => c.String());
            AlterColumn("dbo.Projects", "Name", c => c.String());
            DropColumn("dbo.Projects", "QuotationAgreementApprovedby");
            DropColumn("dbo.Projects", "QuotationRef");
        }
    }
}
