namespace EasyGeneratorAdditionalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SSIQstringtobool : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.SingleSelectImageQuestions", "IsAnswer", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SingleSelectImageQuestions", "IsAnswer", c => c.String());
        }
    }
}
