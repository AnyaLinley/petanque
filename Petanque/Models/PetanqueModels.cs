using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Petanque.Models
{

    public class PetanqueContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Game> Games { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Winner1Games)
                .WithRequired(g => g.Winner1)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
               .HasMany(u => u.Winner2Games)
               .WithRequired(g => g.Winner2)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
               .HasMany(u => u.Winner3Games)
               .WithRequired(g => g.Winner3)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
               .HasMany(u => u.Loser1Games)
               .WithRequired(g => g.Loser1)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
               .HasMany(u => u.Loser2Games)
               .WithRequired(g => g.Loser2)
               .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
               .HasMany(u => u.Loser3Games)
               .WithRequired(g => g.Loser3)
               .WillCascadeOnDelete(false);
        }
    }

    public class User
    {
        [Key, Required, Display(Name = "ID")]
        [ScaffoldColumn(false)]
        public int UserID { get; set; }

        [Required, StringLength(50), Display(Name = "Full Name")]
        public string Name { get; set; }

        [EnumDataType(typeof(AgeBracket)), Display(Name = "Age Bracket")]
        public AgeBracket AgeRange { get; set; }

        [Required, Display(Name = "Test Score")]
        [ScaffoldColumn(false)]
        public int TestScore { get; set; }

        // [NotMapped] properties to be caluculated on the fly for the chart data based on selected league
        [NotMapped]
        [ScaffoldColumn(false)]
        public int Score { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        public int NumGamesWon { get; set; }

        [NotMapped]
        [ScaffoldColumn(false)]
        public int NumGamesLost { get; set; }

        [InverseProperty("Winner1")]
        public virtual ICollection<Game> Winner1Games { get; set; }

        [InverseProperty("Winner2")]
        public virtual ICollection<Game> Winner2Games { get; set; }

        [InverseProperty("Winner3")]
        public virtual ICollection<Game> Winner3Games { get; set; }

        [InverseProperty("Loser1")]
        public virtual ICollection<Game> Loser1Games { get; set; }

        [InverseProperty("Loser2")]
        public virtual ICollection<Game> Loser2Games { get; set; }

        [InverseProperty("Loser3")]
        public virtual ICollection<Game> Loser3Games { get; set; }
    }

    public class Game
    {
        [Key, Display(Name = "ID")]
        [ScaffoldColumn(false)]
        public int GameID { get; set; }

        [Required, ForeignKey("Winner1"), Display(Name = "Winner 1")]
        public int Winner1Id { get; set; }
        public virtual User Winner1 { get; set; }

        [Required, ForeignKey("Winner2"), Display(Name = "Winner 2")]
        public int Winner2Id { get; set; }
        public virtual User Winner2 { get; set; }

        [Required,ForeignKey("Winner3"), Display(Name = "Winner 3")]
        public int Winner3Id { get; set; }
        public virtual User Winner3 { get; set; }

        [Required, ForeignKey("Loser1"), Display(Name = "Loser 1")]
        public int Loser1Id { get; set; }
        public virtual User Loser1 { get; set; }

        [Required, ForeignKey("Loser2"), Display(Name = "Loser 2")]
        public int Loser2Id { get; set; }
        public virtual User Loser2 { get; set; }

        [Required, ForeignKey("Loser3"), Display(Name = "Loser 3")]
        public int Loser3Id { get; set; }
        public virtual User Loser3 { get; set; }

        [Required, Range(0, 12, ErrorMessage = "Value for {0} must be between {1} and {2}.")]
        public int LosingScore { get; set; }

        public int ScoreDifference
        {
            get { return 13 - LosingScore; }
        }

        [Required, DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        public DateTime Date { get; set; }

        [Required]
        public LeagueType League { get; set; }
    }

    public enum AgeBracket { Junior, Adult, Senior }
    public enum LeagueType { Spring, Summer, Autumn, Winter }

}