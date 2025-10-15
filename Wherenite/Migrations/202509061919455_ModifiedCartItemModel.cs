namespace Wherenite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifiedCartItemModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CartItems", "TicketCategoryId", c => c.String());
            AddColumn("dbo.CartItems", "TicketCategoryPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.CartItems", "TicketCategoryPrice");
            DropColumn("dbo.CartItems", "TicketCategoryId");
        }
    }
}
