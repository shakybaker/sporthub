namespace Sporthub.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Resorts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Slug = c.String(),
                        CountryId = c.Int(nullable: false),
                        CountryName = c.String(),
                        TrailsGreen = c.Int(nullable: false),
                        TrailsBlue = c.Int(nullable: false),
                        TrailsRed = c.Int(nullable: false),
                        TrailsBlack = c.Int(nullable: false),
                        LiftsTotal = c.Int(nullable: false),
                        ElevationFrom = c.Int(nullable: false),
                        ElevationTo = c.Int(nullable: false),
                        VerticalDrop = c.Int(nullable: false),
                        LiftTram = c.Int(nullable: false),
                        LiftFastEight = c.Int(nullable: false),
                        LiftFastSix = c.Int(nullable: false),
                        LiftFastQuad = c.Int(nullable: false),
                        LiftQuad = c.Int(nullable: false),
                        LiftDouble = c.Int(nullable: false),
                        LiftSurface = c.Int(nullable: false),
                        LiftTriple = c.Int(nullable: false),
                        RunsLength = c.Int(nullable: false),
                        TerrainParks = c.Int(nullable: false),
                        LongestRun = c.Int(nullable: false),
                        SkiableTerrain = c.Int(nullable: false),
                        ResortStatus = c.String(),
                        SeasonStart = c.String(),
                        SeasonEnd = c.String(),
                        TouristAddress = c.String(),
                        TouristEmail = c.String(),
                        Aka = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Resorts");
        }
    }
}
