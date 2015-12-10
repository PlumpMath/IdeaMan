namespace IdeaManMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Category : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IdeaEntries", "Category", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IdeaEntries", "Category");
        }
    }
}
