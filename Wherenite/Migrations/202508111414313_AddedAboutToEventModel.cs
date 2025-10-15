namespace Wherenite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedAboutToEventModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Events", "About", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Events", "About", c => c.Int(nullable: false));
        }
    }
}
