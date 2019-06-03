namespace StudentManagement_ASP.NET_MCV5.Migrations
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Web.Security;
    using StudentManagement_ASP.NET_MCV5.Models;
    using StudentManagement_ASP.NET_MCV5.Controllers;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;

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
            foreach (var role in Enum.GetNames(typeof(ApplicationRole)))
            {
                IdentityRole identityRole = new IdentityRole(role.ToString());
                var dbRoles = context.Roles.Where(c => c.Name == identityRole.Name).FirstOrDefault();
                if (dbRoles is null)
                {
                    context.Roles.AddOrUpdate(identityRole);
                }
            }

            //HoanLK Add default admin account
           if (!context.Users.Any(u => u.UserName == "Admin"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser
                {
                    Id = "admin@gmail.com",
                    Address = "Ho Chi Minh City",
                    BirthDay = DateTime.Now,
                    Email = "admin@gmail.com",
                    FirstName = "Admin",
                    LastName = "1",
                    UserName = "admin@gmail.com"
                };

                manager.Create(user, "123Aa.");
                manager.AddToRole(user.Id, ApplicationRole.Administrator.ToString());
            }
        }
    }
}
