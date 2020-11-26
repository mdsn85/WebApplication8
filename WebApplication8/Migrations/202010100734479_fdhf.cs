namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fdhf : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.Class1");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Class1",
                c => new
                    {
                        Class1Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Class1Id);
            
        }
    }
}
