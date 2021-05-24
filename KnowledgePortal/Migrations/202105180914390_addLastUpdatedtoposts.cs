namespace KnowledgePortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLastUpdatedtoposts : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "LastUpdated", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Posts", "LastUpdated");
        }
    }
}
