namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class djd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LpoTerms",
                c => new
                    {
                        LpoTermsId = c.Int(nullable: false, identity: true),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.LpoTermsId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.LpoTerms");
        }
    }
}
