namespace IdeaManMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Renamedideas : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.IdeaModels", newName: "IdeaEntries");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.IdeaEntries", newName: "IdeaModels");
        }
    }
}
