namespace Sporthub.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNullableToResort : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Resorts", "TrailsGreen", c => c.Int());
            AlterColumn("dbo.Resorts", "TrailsBlue", c => c.Int());
            AlterColumn("dbo.Resorts", "TrailsRed", c => c.Int());
            AlterColumn("dbo.Resorts", "TrailsBlack", c => c.Int());
            AlterColumn("dbo.Resorts", "LiftsTotal", c => c.Int());
            AlterColumn("dbo.Resorts", "ElevationFrom", c => c.Int());
            AlterColumn("dbo.Resorts", "ElevationTo", c => c.Int());
            AlterColumn("dbo.Resorts", "VerticalDrop", c => c.Int());
            AlterColumn("dbo.Resorts", "LiftTram", c => c.Int());
            AlterColumn("dbo.Resorts", "LiftFastEight", c => c.Int());
            AlterColumn("dbo.Resorts", "LiftFastSix", c => c.Int());
            AlterColumn("dbo.Resorts", "LiftFastQuad", c => c.Int());
            AlterColumn("dbo.Resorts", "LiftQuad", c => c.Int());
            AlterColumn("dbo.Resorts", "LiftDouble", c => c.Int());
            AlterColumn("dbo.Resorts", "LiftSurface", c => c.Int());
            AlterColumn("dbo.Resorts", "LiftTriple", c => c.Int());
            AlterColumn("dbo.Resorts", "RunsLength", c => c.Int());
            AlterColumn("dbo.Resorts", "TerrainParks", c => c.Int());
            AlterColumn("dbo.Resorts", "LongestRun", c => c.Int());
            AlterColumn("dbo.Resorts", "SkiableTerrain", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Resorts", "SkiableTerrain", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "LongestRun", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "TerrainParks", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "RunsLength", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "LiftTriple", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "LiftSurface", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "LiftDouble", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "LiftQuad", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "LiftFastQuad", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "LiftFastSix", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "LiftFastEight", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "LiftTram", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "VerticalDrop", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "ElevationTo", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "ElevationFrom", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "LiftsTotal", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "TrailsBlack", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "TrailsRed", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "TrailsBlue", c => c.Int(nullable: false));
            AlterColumn("dbo.Resorts", "TrailsGreen", c => c.Int(nullable: false));
        }
    }
}
