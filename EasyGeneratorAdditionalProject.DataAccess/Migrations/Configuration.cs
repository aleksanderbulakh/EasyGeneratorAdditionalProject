namespace EasyGeneratorAdditionalProject.DataAccess.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EasyGeneratorAdditionalProject.DataAccess.Context.DatabaseContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(EasyGeneratorAdditionalProject.DataAccess.Context.DatabaseContext context)
        { }
    }
}
