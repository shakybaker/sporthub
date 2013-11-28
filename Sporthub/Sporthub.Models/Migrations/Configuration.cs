namespace Sporthub.Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Sporthub.Models.SporthubContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Sporthub.Models.SporthubContext";
        }

        protected override void Seed(Sporthub.Models.SporthubContext context)
        {
            context.Countries.AddOrUpdate(
                new Country {  Id = 1, Name = "France", Slug = "france" },
                new Country {  Id = 2, Name = "Austria", Slug = "austria" },
                new Country {  Id = 3, Name = "Italy", Slug = "italy" },
                new Country {  Id = 4, Name = "Switzerland", Slug = "switzerland" },
                new Country {  Id = 5, Name = "United Kingdom", Slug = "united-kingdom" },
                new Country {  Id = 6, Name = "Bulgaria", Slug = "bulgaria" },
                new Country {  Id = 7, Name = "Spain", Slug = "spain" },
                new Country {  Id = 8, Name = "Germany", Slug = "germany" },
                new Country {  Id = 9, Name = "Romania", Slug = "romania" },
                new Country {  Id = 10, Name = "Czech Rep.", Slug = "czech-rep" },
                new Country {  Id = 11, Name = "Norway", Slug = "norway" },
                new Country {  Id = 12, Name = "Sweden", Slug = "sweden" },
                new Country {  Id = 13, Name = "Finland", Slug = "finland" },
                new Country {  Id = 14, Name = "Slovenia", Slug = "slovenia" },
                new Country {  Id = 15, Name = "Slovakia", Slug = "slovakia" },
                new Country {  Id = 16, Name = "United States", Slug = "united-states" },
                new Country {  Id = 17, Name = "Canada", Slug = "canada" },
                new Country {  Id = 18, Name = "Australia", Slug = "australia" },
                new Country {  Id = 19, Name = "New Zealand", Slug = "new-zealand" },
                new Country {  Id = 20, Name = "Chile", Slug = "chile" },
                new Country {  Id = 21, Name = "Argentina", Slug = "argentina" }
            );
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //
        }
    }
}
