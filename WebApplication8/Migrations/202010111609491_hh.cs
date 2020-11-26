namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hh : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApprovalTypes",
                c => new
                    {
                        ApprovalTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.ApprovalTypeId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApprovalTypes");
        }
    }
}
