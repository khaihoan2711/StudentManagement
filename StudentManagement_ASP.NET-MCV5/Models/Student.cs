using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace StudentManagement_ASP.NET_MCV5.Models
{
    public class Student : ApplicationUser
    {
        
        public Student()
        {

        }

        public Student(ApplicationUser applicationUser)
        {
            foreach (PropertyInfo propertyInfo in applicationUser.GetType().GetProperties())
            {
                propertyInfo.SetValue(this, propertyInfo.GetValue(applicationUser, null));
            }
        }

        //Student identity number
        [Key]
        public string StudentId { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}