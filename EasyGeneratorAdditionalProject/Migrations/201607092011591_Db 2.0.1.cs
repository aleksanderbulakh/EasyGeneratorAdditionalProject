namespace EasyGeneratorAdditionalProject.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Db201 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Contents",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        SectionId = c.Guid(nullable: false),
                        Title = c.String(),
                        Type = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sections", t => t.SectionId, cascadeDelete: true)
                .Index(t => t.SectionId);
            
            CreateIndex("dbo.Materials", "ContentId");
            CreateIndex("dbo.MultipleSelectQuestions", "ContentId");
            CreateIndex("dbo.SingleSelectImageQuestions", "ContentId");
            CreateIndex("dbo.SingleSelectQuestions", "ContentId");
            AddForeignKey("dbo.Materials", "ContentId", "dbo.Contents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.MultipleSelectQuestions", "ContentId", "dbo.Contents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SingleSelectImageQuestions", "ContentId", "dbo.Contents", "Id", cascadeDelete: true);
            AddForeignKey("dbo.SingleSelectQuestions", "ContentId", "dbo.Contents", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SingleSelectQuestions", "ContentId", "dbo.Contents");
            DropForeignKey("dbo.SingleSelectImageQuestions", "ContentId", "dbo.Contents");
            DropForeignKey("dbo.Contents", "SectionId", "dbo.Sections");
            DropForeignKey("dbo.MultipleSelectQuestions", "ContentId", "dbo.Contents");
            DropForeignKey("dbo.Materials", "ContentId", "dbo.Contents");
            DropIndex("dbo.SingleSelectQuestions", new[] { "ContentId" });
            DropIndex("dbo.SingleSelectImageQuestions", new[] { "ContentId" });
            DropIndex("dbo.MultipleSelectQuestions", new[] { "ContentId" });
            DropIndex("dbo.Materials", new[] { "ContentId" });
            DropIndex("dbo.Contents", new[] { "SectionId" });
            DropTable("dbo.Contents");
        }
    }
}
