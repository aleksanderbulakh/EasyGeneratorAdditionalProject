namespace EasyGeneratorAdditionalProject.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Singleselectimageanswertablel : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Photos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Photo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Answers", "Photo_Id", c => c.Guid());
            CreateIndex("dbo.Answers", "Photo_Id");
            AddForeignKey("dbo.Answers", "Photo_Id", "dbo.Photos", "Id");
            DropColumn("dbo.Answers", "Photo");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answers", "Photo", c => c.String());
            DropForeignKey("dbo.Answers", "Photo_Id", "dbo.Photos");
            DropIndex("dbo.Answers", new[] { "Photo_Id" });
            DropColumn("dbo.Answers", "Photo_Id");
            DropTable("dbo.Photos");
        }
    }
}
