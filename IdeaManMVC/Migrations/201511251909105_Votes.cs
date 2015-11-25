namespace IdeaManMVC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Votes : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Votes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        DateCreated = c.DateTime(),
                        UserCreated = c.String(),
                        DateModified = c.DateTime(),
                        UserModified = c.String(),
                        Idea_Id = c.Int(),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdeaEntries", t => t.Idea_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Idea_Id)
                .Index(t => t.User_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Votes", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Votes", "Idea_Id", "dbo.IdeaEntries");
            DropIndex("dbo.Votes", new[] { "User_Id" });
            DropIndex("dbo.Votes", new[] { "Idea_Id" });
            DropTable("dbo.Votes");
        }
    }
}
