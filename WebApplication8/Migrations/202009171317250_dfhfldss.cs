namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhfldss : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Designers",
                c => new
                    {
                        DesignerId = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Mobile = c.String(),
                        Email = c.String(),
                        EmployeeId = c.Int(),
                    })
                .PrimaryKey(t => t.DesignerId)
                .ForeignKey("dbo.Employees", t => t.EmployeeId)
                .Index(t => t.EmployeeId);
            
            AddColumn("dbo.Projects", "DesignerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Projects", "DesignerId");
            AddForeignKey("dbo.Projects", "DesignerId", "dbo.Designers", "DesignerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Projects", "DesignerId", "dbo.Designers");
            DropForeignKey("dbo.Designers", "EmployeeId", "dbo.Employees");
            DropIndex("dbo.Designers", new[] { "EmployeeId" });
            DropIndex("dbo.Projects", new[] { "DesignerId" });
            DropColumn("dbo.Projects", "DesignerId");
            DropTable("dbo.Designers");
        }
    }
}
