namespace Version01.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixDateIssue : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.People", "LastContacted", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.People", "LastContacted", c => c.DateTime(nullable: false));
        }
    }
}
