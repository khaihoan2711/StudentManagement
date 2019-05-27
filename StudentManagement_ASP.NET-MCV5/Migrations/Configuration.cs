namespace StudentManagement_ASP.NET_MCV5.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;

    internal sealed class Configuration : DbMigrationsConfiguration<StudentManagement_ASP.NET_MCV5.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            ContextKey = "StudentManagement_ASP.NET_MCV5.Models.ApplicationDbContext";
        }

        protected override void Seed(StudentManagement_ASP.NET_MCV5.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            //HoanLK init default data for table Roles in DB
            List<IdentityRole> roles = new List<IdentityRole>();
            roles.Add(new IdentityRole("Administrator"));
            roles.Add(new IdentityRole("Student"));
            roles.Add(new IdentityRole("Lecturer"));                        
            foreach (var role in roles)
            {
                var dbRoles = context.Roles.Where(c => c.Name == role.Name).FirstOrDefault();
                if (dbRoles is null)
                {
                    context.Roles.AddOrUpdate(role);
                }
            }
        }
    }
}
