namespace EasyGeneratorAdditionalProject.DataAccess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class normalsingleselectimageanswerobject : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Answers", "Photo_Id", "dbo.Photos");
            DropIndex("dbo.Answers", new[] { "Photo_Id" });
            DropColumn("dbo.Answers", "Photo_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Answers", "Photo_Id", c => c.Guid());
            CreateIndex("dbo.Answers", "Photo_Id");
            AddForeignKey("dbo.Answers", "Photo_Id", "dbo.Photos", "Id");
        }
    }
}
