namespace AlbumService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using AlbumService.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<AlbumService.Models.AlbumServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AlbumService.Models.AlbumServiceContext context)
        {
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

            context.Artists.AddOrUpdate(x => x.Id,
                new Artist() { Id = 1, Name = "Depeche Mode" },
                new Artist() { Id = 2, Name = "Zero 7" },
                new Artist() { Id = 3, Name = "Brandi Carlile" }
                );

            context.Albums.AddOrUpdate(x => x.Id,
                new Album() { Id = 1, Title = "Music for the Masses", Year = 1987, ArtistId = 1, Genre = "SynthPop", Price = 9.99M },
                new Album() { Id = 1, Title = "Violator", Year = 1990, ArtistId = 1, Genre = "SynthPop", Price = 11.99M },
                new Album() { Id = 1, Title = "Simple Things", Year = 2001, ArtistId = 2, Genre = "Electronica", Price = 8.99M },
                new Album() { Id = 1, Title = "The Story", Year = 2007, ArtistId = 3, Genre = "Folk", Price = 15.99M }
                );


        }
    }
}
