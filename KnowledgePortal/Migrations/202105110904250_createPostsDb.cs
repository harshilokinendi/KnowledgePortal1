namespace KnowledgePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createPostsDb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.Int(nullable: false),
                        TagNames = c.String(),
                        Technology = c.String(),
                        Summary = c.String(),
                        Description = c.String(),
                        CreatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Posts");
        }
    }
}
