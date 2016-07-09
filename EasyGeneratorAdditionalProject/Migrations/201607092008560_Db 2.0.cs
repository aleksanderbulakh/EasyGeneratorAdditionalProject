namespace EasyGeneratorAdditionalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db20 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Questions", "QuestionTypeId", "dbo.QuestionTypes");
            DropForeignKey("dbo.Questions", "SectionId", "dbo.Sections");
            DropIndex("dbo.Questions", new[] { "SectionId" });
            DropIndex("dbo.Questions", new[] { "QuestionTypeId" });
            CreateTable(
                "dbo.Materials",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContentId = c.Guid(nullable: false),
                        Text = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MultipleSelectQuestions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContentId = c.Guid(nullable: false),
                        Text = c.String(),
                        IsAnswer = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SingleSelectImageQuestions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContentId = c.Guid(nullable: false),
                        Text = c.String(),
                        IsAnswer = c.String(),
                        Photo = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.SingleSelectQuestions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        ContentId = c.Guid(nullable: false),
                        Text = c.String(),
                        IsAnswer = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.Questions");
            DropTable("dbo.QuestionTypes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.QuestionTypes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Questions",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SectionId = c.Guid(nullable: false),
                        QuestionTypeId = c.Guid(nullable: false),
                        Title = c.String(),
                        Answer = c.Int(nullable: false),
                        AnswerVariants = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropTable("dbo.SingleSelectQuestions");
            DropTable("dbo.SingleSelectImageQuestions");
            DropTable("dbo.MultipleSelectQuestions");
            DropTable("dbo.Materials");
            CreateIndex("dbo.Questions", "QuestionTypeId");
            CreateIndex("dbo.Questions", "SectionId");
            AddForeignKey("dbo.Questions", "SectionId", "dbo.Sections", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Questions", "QuestionTypeId", "dbo.QuestionTypes", "Id", cascadeDelete: true);
        }
    }
}
