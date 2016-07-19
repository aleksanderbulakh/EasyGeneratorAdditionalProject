namespace EasyGeneratorAdditionalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FluentAPIv2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.MultipleSelectAnswers", "IsCorrect", c => c.Boolean(nullable: false));
            AddColumn("dbo.SingleSelectImageAnswers", "IsCorrect", c => c.Boolean(nullable: false));
            AddColumn("dbo.SingleSelectAnswers", "IsCorrect", c => c.Boolean(nullable: false));
            DropColumn("dbo.MultipleSelectAnswers", "IsAnswer");
            DropColumn("dbo.SingleSelectImageAnswers", "IsAnswer");
            DropColumn("dbo.SingleSelectAnswers", "IsAnswer");
        }
        
        public override void Down()
        {
            AddColumn("dbo.SingleSelectAnswers", "IsAnswer", c => c.Boolean(nullable: false));
            AddColumn("dbo.SingleSelectImageAnswers", "IsAnswer", c => c.Boolean(nullable: false));
            AddColumn("dbo.MultipleSelectAnswers", "IsAnswer", c => c.Boolean(nullable: false));
            DropColumn("dbo.SingleSelectAnswers", "IsCorrect");
            DropColumn("dbo.SingleSelectImageAnswers", "IsCorrect");
            DropColumn("dbo.MultipleSelectAnswers", "IsCorrect");
        }
    }
}
