namespace EasyGeneratorAdditionalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class titlerequired : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Content", "Title", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Content", "Type", c => c.String(nullable: false));
            AlterColumn("dbo.Materials", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.MultipleSelectAnswers", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.Sections", "Title", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Courses", "Title", c => c.String(nullable: false, maxLength: 255));
            AlterColumn("dbo.Users", "FirstName", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Surname", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "Email", c => c.String(nullable: false));
            AlterColumn("dbo.Users", "PasswordHash", c => c.String(nullable: false));
            AlterColumn("dbo.SingleSelectAnswers", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.SingleSelectImageAnswers", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.SingleSelectImageAnswers", "Photo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.SingleSelectImageAnswers", "Photo", c => c.String());
            AlterColumn("dbo.SingleSelectImageAnswers", "Text", c => c.String());
            AlterColumn("dbo.SingleSelectAnswers", "Text", c => c.String());
            AlterColumn("dbo.Users", "PasswordHash", c => c.String());
            AlterColumn("dbo.Users", "Email", c => c.String());
            AlterColumn("dbo.Users", "Surname", c => c.String());
            AlterColumn("dbo.Users", "FirstName", c => c.String());
            AlterColumn("dbo.Courses", "Title", c => c.String(maxLength: 255));
            AlterColumn("dbo.Sections", "Title", c => c.String(maxLength: 255));
            AlterColumn("dbo.MultipleSelectAnswers", "Text", c => c.String());
            AlterColumn("dbo.Materials", "Text", c => c.String());
            AlterColumn("dbo.Content", "Type", c => c.String());
            AlterColumn("dbo.Content", "Title", c => c.String(maxLength: 255));
        }
    }
}
