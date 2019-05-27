namespace StudentManagement_ASP.NET_MCV5.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updatedatabaseuser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "AccountType", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "UserType");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "UserType", c => c.Int(nullable: false));
            DropColumn("dbo.AspNetUsers", "AccountType");
        }
    }
}
