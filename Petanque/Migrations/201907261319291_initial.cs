namespace Petanque.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Games",
                c => new
                    {
                        GameID = c.Int(nullable: false, identity: true),
                        Winner1Id = c.Int(nullable: false),
                        Winner2Id = c.Int(nullable: false),
                        Winner3Id = c.Int(nullable: false),
                        Loser1Id = c.Int(nullable: false),
                        Loser2Id = c.Int(nullable: false),
                        Loser3Id = c.Int(nullable: false),
                        LosingScore = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        League = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.GameID)
                .ForeignKey("dbo.Users", t => t.Loser1Id)
                .ForeignKey("dbo.Users", t => t.Loser2Id)
                .ForeignKey("dbo.Users", t => t.Loser3Id)
                .ForeignKey("dbo.Users", t => t.Winner1Id)
                .ForeignKey("dbo.Users", t => t.Winner2Id)
                .ForeignKey("dbo.Users", t => t.Winner3Id)
                .Index(t => t.Winner1Id)
                .Index(t => t.Winner2Id)
                .Index(t => t.Winner3Id)
                .Index(t => t.Loser1Id)
                .Index(t => t.Loser2Id)
                .Index(t => t.Loser3Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        AgeRange = c.Int(nullable: false),
                        TestScore = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Games", "Winner3Id", "dbo.Users");
            DropForeignKey("dbo.Games", "Winner2Id", "dbo.Users");
            DropForeignKey("dbo.Games", "Winner1Id", "dbo.Users");
            DropForeignKey("dbo.Games", "Loser3Id", "dbo.Users");
            DropForeignKey("dbo.Games", "Loser2Id", "dbo.Users");
            DropForeignKey("dbo.Games", "Loser1Id", "dbo.Users");
            DropIndex("dbo.Games", new[] { "Loser3Id" });
            DropIndex("dbo.Games", new[] { "Loser2Id" });
            DropIndex("dbo.Games", new[] { "Loser1Id" });
            DropIndex("dbo.Games", new[] { "Winner3Id" });
            DropIndex("dbo.Games", new[] { "Winner2Id" });
            DropIndex("dbo.Games", new[] { "Winner1Id" });
            DropTable("dbo.Users");
            DropTable("dbo.Games");
        }
    }
}
