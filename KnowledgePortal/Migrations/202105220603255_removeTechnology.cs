namespace KnowledgePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeTechnology : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Posts", "Summary", c => c.String(nullable: false, maxLength: 150));
            DropColumn("dbo.Posts", "Technology");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Posts", "Technology", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Summary", c => c.String(nullable: false));
        }
    }
}
