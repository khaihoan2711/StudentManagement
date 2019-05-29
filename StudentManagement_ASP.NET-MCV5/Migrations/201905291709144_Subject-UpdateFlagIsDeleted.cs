namespace StudentManagement_ASP.NET_MCV5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SubjectUpdateFlagIsDeleted : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Subjects", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Subjects", "IsDeleted");
        }
    }
}
