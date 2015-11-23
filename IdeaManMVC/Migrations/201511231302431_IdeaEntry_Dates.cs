namespace IdeaManMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IdeaEntry_Dates : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IdeaEntries", "DateCreated", c => c.DateTime());
            AddColumn("dbo.IdeaEntries", "UserCreated", c => c.String());
            AddColumn("dbo.IdeaEntries", "DateModified", c => c.DateTime());
            AddColumn("dbo.IdeaEntries", "UserModified", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IdeaEntries", "UserModified");
            DropColumn("dbo.IdeaEntries", "DateModified");
            DropColumn("dbo.IdeaEntries", "UserCreated");
            DropColumn("dbo.IdeaEntries", "DateCreated");
        }
    }
}
