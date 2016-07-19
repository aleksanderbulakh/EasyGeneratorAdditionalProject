namespace EasyGeneratorAdditionalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FluentAPI : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Contents", newName: "Content");
            RenameTable(name: "dbo.SingleSelectImageQuestions", newName: "MultipleSelectAnswers");
            RenameTable(name: "dbo.SingleSelectQuestions", newName: "SingleSelectAnswers");
            RenameTable(name: "dbo.MultipleSelectQuestions", newName: "SingleSelectImageAnswers");
            AddColumn("dbo.SingleSelectImageAnswers", "Photo", c => c.String());
            AlterColumn("dbo.Content", "Title", c => c.String(maxLength: 255));
            AlterColumn("dbo.Sections", "Title", c => c.String(maxLength: 255));
            AlterColumn("dbo.Courses", "Title", c => c.String(maxLength: 255));
            DropColumn("dbo.Users", "UserName");
            DropColumn("dbo.MultipleSelectAnswers", "Photo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.MultipleSelectAnswers", "Photo", c => c.String());
            AddColumn("dbo.Users", "UserName", c => c.String());
            AlterColumn("dbo.Courses", "Title", c => c.String());
            AlterColumn("dbo.Sections", "Title", c => c.String());
            AlterColumn("dbo.Content", "Title", c => c.String());
            DropColumn("dbo.SingleSelectImageAnswers", "Photo");
            RenameTable(name: "dbo.SingleSelectImageAnswers", newName: "MultipleSelectQuestions");
            RenameTable(name: "dbo.SingleSelectAnswers", newName: "SingleSelectQuestions");
            RenameTable(name: "dbo.MultipleSelectAnswers", newName: "SingleSelectImageQuestions");
            RenameTable(name: "dbo.Content", newName: "Contents");
        }
    }
}
