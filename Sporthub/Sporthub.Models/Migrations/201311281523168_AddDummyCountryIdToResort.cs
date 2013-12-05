namespace Sporthub.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddDummyCountryIdToResort : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Resorts", "DummyCountryId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Resorts", "DummyCountryId");
        }
    }
}
