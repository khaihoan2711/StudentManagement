using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace StudentManagement_ASP.NET_MCV5.Models
{
    public class Student : ApplicationUser
    {
        public Student()
        {
            this.Classes = new HashSet<Class>();
        }

        //Student identity number
        [Required]
        [Key]
        public string StudentId { get; set; }
        public DateTime? EnrollmentDate { get; set; }
        public virtual ICollection<Class> Classes { get; set; }
    }
}