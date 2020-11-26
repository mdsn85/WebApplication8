namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw20 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomersTypes",
                c => new
                    {
                        CustomersTypeId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.CustomersTypeId);
            
            AddColumn("dbo.Projects", "CustomersTypeId", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "CustomersTypeId");
            AddForeignKey("dbo.Projects", "CustomersTypeId", "dbo.CustomersTypes", "CustomersTypeId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "CustomersTypeId", "dbo.CustomersTypes");
            DropIndex("dbo.Projects", new[] { "CustomersTypeId" });
            DropColumn("dbo.Projects", "CustomersTypeId");
            DropTable("dbo.CustomersTypes");
        }
    }
}
