namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dhjdww : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LpoSequenses",
                c => new
                    {
                        LpoSequenseId = c.Int(nullable: false, identity: true),
                        Sequense = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.LpoSequenseId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LpoSequenses");
        }
    }
}
