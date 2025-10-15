namespace Wherenite.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedPhoneNumber : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Venues", "PhoneNumber1", c => c.String());
            AddColumn("dbo.Venues", "PhoneNumber2", c => c.String());
            AddColumn("dbo.Venues", "PhoneNumber3", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Venues", "PhoneNumber3");
            DropColumn("dbo.Venues", "PhoneNumber2");
            DropColumn("dbo.Venues", "PhoneNumber1");
        }
    }
}
