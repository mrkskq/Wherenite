namespace Wherenite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCartItemModelAndTicketModel : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Tickets", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CartItems", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.CartItems", new[] { "UserId" });
            DropIndex("dbo.Tickets", new[] { "UserId" });
            AlterColumn("dbo.CartItems", "UserId", c => c.String());
            AlterColumn("dbo.Tickets", "UserId", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Tickets", "UserId", c => c.String(maxLength: 128));
            AlterColumn("dbo.CartItems", "UserId", c => c.String(maxLength: 128));
            CreateIndex("dbo.Tickets", "UserId");
            CreateIndex("dbo.CartItems", "UserId");
            AddForeignKey("dbo.CartItems", "UserId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Tickets", "UserId", "dbo.AspNetUsers", "Id");
        }
    }
}
