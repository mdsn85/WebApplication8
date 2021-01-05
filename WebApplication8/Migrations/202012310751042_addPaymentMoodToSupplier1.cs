﻿namespace WebApplication8.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addPaymentMoodToSupplier1 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.suppliers", "TrnNo", c => c.String(maxLength: 25));
        }
        
        public override void Down()
        {
            DropColumn("dbo.suppliers", "TrnNo");
        }
    }
}
