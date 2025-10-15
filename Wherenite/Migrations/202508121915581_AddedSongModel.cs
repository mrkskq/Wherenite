namespace Wherenite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedSongModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Songs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SongURL = c.String(),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Songs", "EventId", "dbo.Events");
            DropIndex("dbo.Songs", new[] { "EventId" });
            DropTable("dbo.Songs");
        }
    }
}
