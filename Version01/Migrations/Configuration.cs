namespace Version01.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Version01.PersonData>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "Version01.PersonData";
        }

        protected override void Seed(Version01.PersonData context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data.
        }
    }
}
