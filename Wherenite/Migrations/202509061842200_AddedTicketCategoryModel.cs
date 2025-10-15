namespace Wherenite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedTicketCategoryModel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TicketCategories",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        EventId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Events", t => t.EventId, cascadeDelete: true)
                .Index(t => t.EventId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.TicketCategories", "EventId", "dbo.Events");
            DropIndex("dbo.TicketCategories", new[] { "EventId" });
            DropTable("dbo.TicketCategories");
        }
    }
}
