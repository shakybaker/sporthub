namespace Sporthub.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RemovedCountryRefFromResort : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Resorts", "CountryId");
            DropColumn("dbo.Resorts", "CountryName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Resorts", "CountryName", c => c.String());
            AddColumn("dbo.Resorts", "CountryId", c => c.Int(nullable: false));
        }
    }
}
