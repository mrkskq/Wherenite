namespace Wherenite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class editedEvent : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Events", "EventImage", c => c.String());
            DropColumn("dbo.Events", "Image");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Events", "Image", c => c.String());
            DropColumn("dbo.Events", "EventImage");
        }
    }
}
