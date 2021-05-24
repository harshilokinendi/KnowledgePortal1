namespace KnowledgePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class validationsToPosts : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "TagNames", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Technology", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Summary", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Description", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Posts", "Description", c => c.String());
            AlterColumn("dbo.Posts", "Summary", c => c.String());
            AlterColumn("dbo.Posts", "Technology", c => c.String());
            AlterColumn("dbo.Posts", "TagNames", c => c.String());
        }
    }
}
