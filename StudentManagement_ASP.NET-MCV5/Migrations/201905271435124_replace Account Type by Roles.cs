namespace StudentManagement_ASP.NET_MCV5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class replaceAccountTypebyRoles : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.AspNetUsers", "AccountType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "AccountType", c => c.Int(nullable: false));
        }
    }
}
