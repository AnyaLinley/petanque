namespace Petanque.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using Petanque.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<PetanqueContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(PetanqueContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Users.AddOrUpdate(
                 new User
                 {
                     Name = "[Deleted]",
                     AgeRange = AgeBracket.Adult,
                     TestScore = 0
                 },
                 new User
                 {
                     Name = "[Noone]",
                     AgeRange = AgeBracket.Adult,
                     TestScore = 0
                 },
                 new User
                 {
                     Name = "Betty Shoots",
                     AgeRange = AgeBracket.Senior,
                     TestScore = 19
                 },
                 new User
                 {
                     Name = "Justin Syde",
                     AgeRange = AgeBracket.Junior,
                     TestScore = 12
                 },
                 new User
                 {
                     Name = "Jack Orcoche",
                     AgeRange = AgeBracket.Senior,
                     TestScore = 5
                 },
                 new User
                 {
                     Name = "Ivor Boule",
                     AgeRange = AgeBracket.Adult,
                     TestScore = 11
                 },
                 new User
                 {
                     Name = "Donna Sunhat",
                     AgeRange = AgeBracket.Adult,
                     TestScore = 23
                 },
                 new User
                 {
                     Name = "Fanny Ewetwice",
                     AgeRange = AgeBracket.Senior,
                     TestScore = 15
                 },
                 new User
                 {
                     Name = "Al Point",
                     AgeRange = AgeBracket.Adult,
                     TestScore = 17
                 },
                 new User
                 {
                     Name = "Sunny Piste",
                     AgeRange = AgeBracket.Junior,
                     TestScore = 14
                 },
                 new User
                 {
                     Name = "Izzy Onn",
                     AgeRange = AgeBracket.Adult,
                     TestScore = 7
                 },
                 new User
                 {
                     Name = "Chuck Farr",
                     AgeRange = AgeBracket.Senior,
                     TestScore = 11
                 });

            context.SaveChanges();

            context.Games.AddOrUpdate(
                new Game
                {
                    Winner1Id = 3,
                    Winner2Id = 2,
                    Winner3Id = 2,
                    Loser1Id = 4,
                    Loser2Id = 2,
                    Loser3Id = 2,
                    LosingScore = 7,
                    Date = new DateTime(2019, 7, 1),
                    League = LeagueType.Summer
                },
                new Game
                {
                    Winner1Id = 5,
                    Winner2Id = 2,
                    Winner3Id = 2,
                    Loser1Id = 6,
                    Loser2Id = 2,
                    Loser3Id = 2,
                    LosingScore = 3,
                    Date = new DateTime(2019, 7, 1),
                    League = LeagueType.Summer
                });

            context.SaveChanges();
        }
    }
}
