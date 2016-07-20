namespace EasyGeneratorAdditionalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class parentmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "CreatedBy", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "CreatedBy");
        }
    }
}
