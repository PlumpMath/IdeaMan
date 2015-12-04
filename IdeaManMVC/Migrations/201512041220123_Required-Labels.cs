namespace IdeaManMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RequiredLabels : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.IdeaEntries", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.IdeaEntries", "ShortDescription", c => c.String(nullable: false, maxLength: 180));
            AlterColumn("dbo.IdeaEntries", "FullText", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.IdeaEntries", "FullText", c => c.String());
            AlterColumn("dbo.IdeaEntries", "ShortDescription", c => c.String());
            AlterColumn("dbo.IdeaEntries", "Title", c => c.String());
        }
    }
}
