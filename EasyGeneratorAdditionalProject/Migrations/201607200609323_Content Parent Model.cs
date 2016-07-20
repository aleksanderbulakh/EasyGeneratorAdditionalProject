namespace EasyGeneratorAdditionalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ContentParentModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Content", "CreatedBy", c => c.String());
            AddColumn("dbo.Content", "CreatedOn", c => c.DateTime(nullable: false));
            AddColumn("dbo.Content", "LastModifiedDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Content", "LastModifiedDate");
            DropColumn("dbo.Content", "CreatedOn");
            DropColumn("dbo.Content", "CreatedBy");
        }
    }
}
