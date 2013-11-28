namespace Sporthub.Models.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCountryClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Countries",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(maxLength: 200),
                        Slug = c.String(),
                    })
                    .PrimaryKey(t => t.Id)
                    .Index(p => p.Name, unique: true);
            
            AddColumn("dbo.Resorts", "Country_Id", c => c.Int());
            CreateIndex("dbo.Resorts", "Country_Id");
            AddForeignKey("dbo.Resorts", "Country_Id", "dbo.Countries", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Resorts", "Country_Id", "dbo.Countries");
            DropIndex("dbo.Resorts", new[] { "Country_Id" });
            DropColumn("dbo.Resorts", "Country_Id");
            DropTable("dbo.Countries");
        }
    }
}
