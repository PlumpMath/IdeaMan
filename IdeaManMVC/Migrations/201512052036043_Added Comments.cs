namespace IdeaManMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddedComments : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Text = c.String(nullable: false, maxLength: 500),
                        Author_Id = c.String(nullable: false, maxLength: 128),
                        Idea_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id, cascadeDelete: true)
                .ForeignKey("dbo.IdeaEntries", t => t.Idea_Id, cascadeDelete: true)
                .Index(t => t.Author_Id)
                .Index(t => t.Idea_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "Idea_Id", "dbo.IdeaEntries");
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "Idea_Id" });
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropTable("dbo.Comments");
        }
    }
}
