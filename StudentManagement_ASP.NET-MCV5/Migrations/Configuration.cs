namespace StudentManagement_ASP.NET_MCV5.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using StudentManagement_ASP.NET_MCV5.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

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

            DbUtilities dbUtilities = new DbUtilities();
            dbUtilities.IninializeDatabase(context);

        }


        private class DbUtilities
        {
            //public override void Up()
            //{
            //    Sql("INSERT INTO FACULTIES(ID, NAME, ISDELETED) VALUES ('CNTT',     N'CÔNG NGHỆ THÔNG TIN',     'FALSE')");
            //    Sql("INSERT INTO FACULTIES(ID, NAME, ISDELETED) VALUES ('KT',       N'KINH TẾ',                 'FALSE')");
            //    Sql("INSERT INTO FACULTIES(ID, NAME, ISDELETED) VALUES ('QTKD',     N'QUẢN TRỊ KINH DOANH',     'FALSE')");
            //    Sql("INSERT INTO FACULTIES(ID, NAME, ISDELETED) VALUES ('NN',       N'NGOẠI NGỮ',               'FALSE')");
            //}

            public void IninializeDatabase(StudentManagement_ASP.NET_MCV5.Models.ApplicationDbContext dbContext)
            {
                //HoanLK Insert sample data for Faculties table
                List<Faculty> faculties = new List<Faculty>
                {
                    new Faculty() { Id = "CNTT", Name = "CÔNG NGHỆ THÔNG TIN" },
                    new Faculty() { Id = "KT", Name = "KINH TẾ"},
                    new Faculty() { Id = "QTKD", Name = "QUẢN TRỊ KINH DOANH" },
                    new Faculty() { Id = "NN", Name = "NGOẠI NGỮ" }
                };
                foreach (Faculty item in faculties)
                {
                    if (dbContext.Faculties.Where(c => c.Id == item.Id).FirstOrDefault() == null)
                    {
                        dbContext.Faculties.Add(item);
                    }
                }


                //HoanLK Insert sample data for Subjects table
                List<Subject> subjects = new List<Subject>
                {
                    new Subject(){Id = "0001", Name ="Toán cao cấp 1", Description="Series môn học Toán cao cấp 1,2,3"},
                    new Subject(){Id = "0002", Name ="Toán cao cấp 2", Description="Series môn học Toán cao cấp 1,2,3"},
                    new Subject(){Id = "0003", Name ="Toán cao cấp 3", Description="Series môn học Toán cao cấp 1,2,3"},
                    new Subject(){Id = "0004", Name ="Tiếng Anh 1", Description="Series môn học Tiếng Anh 1,2,3,4,5"},
                    new Subject(){Id = "0005", Name ="Tiếng Anh 2", Description="Series môn học Tiếng Anh 1,2,3,4,5"},
                    new Subject(){Id = "0006", Name ="Tiếng Anh 3", Description="Series môn học Tiếng Anh 1,2,3,4,5"},
                    new Subject(){Id = "0007", Name ="Tiếng Anh 4", Description="Series môn học Tiếng Anh 1,2,3,4,5"},
                    new Subject(){Id = "0008", Name ="Tiếng Anh 5", Description="Series môn học Tiếng Anh 1,2,3,4,5"},
                    new Subject(){Id = "0009", Name ="Lập trình C", Description="Lập trình C"},
                    new Subject(){Id = "0010", Name ="Lập trình OOP", Description="Lập trình hướng đối tượng"},
                    new Subject(){Id = "0011", Name ="Lập trình Web", Description="Lập trình Web"}
                };

                foreach (Subject item in subjects)
                {
                    if (dbContext.Subjects.Where(c => c.Name == item.Name).FirstOrDefault() == null)
                    {
                        dbContext.Subjects.Add(item);
                    }
                }




                //HoanLK init default data for table Roles in DB                
                foreach (string role in Enum.GetNames(typeof(ApplicationRole)))
                {
                    IdentityRole identityRole = new IdentityRole(role.ToString());
                    IdentityRole dbRoles = dbContext.Roles.Where(c => c.Name == identityRole.Name).FirstOrDefault();
                    if (dbRoles is null)
                    {
                        dbContext.Roles.AddOrUpdate(identityRole);
                    }
                }

                //Up();

                //HoanLK Add default admin account
                if (!dbContext.Users.Any(u => u.UserName == "admin@gmail.com"))
                {
                    UserStore<ApplicationUser> store = new UserStore<ApplicationUser>(dbContext);
                    UserManager<ApplicationUser> manager = new UserManager<ApplicationUser>(store);
                    ApplicationUser user = new ApplicationUser
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

                //HoanLK Add default user(lecturer) account
                Lecturer createdLecturer = null;
                if (!dbContext.Users.Any(u => u.UserName == "lecturer@gmail.com"))
                {
                    UserStore<Lecturer> store = new UserStore<Lecturer>(dbContext);
                    UserManager<Lecturer> manager = new UserManager<Lecturer>(store);
                    Lecturer user = new Lecturer
                    {
                        Id = "lecturer@gmail.com",
                        Address = "Ho Chi Minh City",
                        BirthDay = DateTime.Now,
                        Email = "lecturer@gmail.com",
                        FirstName = "lecturer",
                        LastName = "1",
                        UserName = "lecturer@gmail.com",
                        HireDate = DateTime.Now,
                        LecturerCode = "0000000001"
                    };

                    manager.Create(user, "123Aa.");
                    manager.AddToRole(user.Id, ApplicationRole.Administrator.ToString());
                    createdLecturer = user;
                }

                //HoanLK Insert sample data for Classes table
                List<Class> Classes = new List<Class>
                {
                    new Class() { Name = "16HTH01", DegreeLevel = DegreeLevels.AssociateDegree, FacultyId= "CNTT", LecturerId ="lecturer@gmail.com", SchoolYear = "2016", TypeOfEducation = TypeOfEducations.InServices, Faculty= faculties[0], Lecturer= createdLecturer  },
                    new Class() { Name = "16HTH02", DegreeLevel = DegreeLevels.BachelorDegree, FacultyId= "CNTT", LecturerId ="lecturer@gmail.com", SchoolYear = "2016", TypeOfEducation = TypeOfEducations.FullTime, Faculty= faculties[0], Lecturer= createdLecturer  },
                    new Class() { Name = "16HTH03", DegreeLevel = DegreeLevels.MasterDegree, FacultyId= "CNTT", LecturerId ="lecturer@gmail.com", SchoolYear = "2016", TypeOfEducation = TypeOfEducations.FullTime, Faculty= faculties[0], Lecturer= createdLecturer  }
                };
                foreach (Class item in Classes)
                {
                    if (dbContext.Classes.Where(c => c.Id == item.Id).FirstOrDefault() == null)
                    {
                        dbContext.Classes.Add(item);
                    }
                }
                dbContext.SaveChanges();

                if (!System.Diagnostics.Debugger.IsAttached)
                {
                    System.Diagnostics.Debugger.Launch();
                }

                //HoanLK Add default user(student) account
                if (!dbContext.Users.Any(u => u.UserName == "student@gmail.com"))
                {
                    UserStore<Student> store = new UserStore<Student>(dbContext);
                    UserManager<Student> manager = new UserManager<Student>(store);
                    Student user = new Student
                    {
                        Id = "student@gmail.com",
                        Address = "Ho Chi Minh City",
                        BirthDay = DateTime.Now,
                        Email = "student@gmail.com",
                        FirstName = "student",
                        LastName = "1",
                        UserName = "student@gmail.com",
                        EnrollmentDate = DateTime.Now,
                        StudentCode = "0000000002"
                    };

                    manager.Create(user, "123Aa.");
                    manager.AddToRole(user.Id, ApplicationRole.Administrator.ToString());
                    dbContext.SaveChanges();

                    int tmpClassId = dbContext.Classes.FirstOrDefault().Id;
                    Console.WriteLine(tmpClassId);
                    Console.WriteLine(user.Id);
                    dbContext.StudentClasses.Add(new StudentClass()
                    {
                        StudentId = user.Id,
                        ClassId = tmpClassId
                    });
                }

                dbContext.SaveChanges();
            }
        }
    }
}
