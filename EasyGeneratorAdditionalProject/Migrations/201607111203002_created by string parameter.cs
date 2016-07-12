namespace EasyGeneratorAdditionalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class createdbystringparameter : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Sections", "CreatedBy", c => c.String());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Sections", "CreatedBy", c => c.DateTime(nullable: false));
        }
    }
}
