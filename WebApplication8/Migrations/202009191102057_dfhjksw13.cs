namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dfhjksw13 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Project2", "DesignerId", c => c.Int(nullable: false));
            CreateIndex("dbo.Project2", "DesignerId");
            AddForeignKey("dbo.Project2", "DesignerId", "dbo.Designers", "DesignerId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Project2", "DesignerId", "dbo.Designers");
            DropIndex("dbo.Project2", new[] { "DesignerId" });
            DropColumn("dbo.Project2", "DesignerId");
        }
    }
}
