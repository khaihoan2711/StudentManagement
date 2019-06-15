using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace StudentManagement_ASP.NET_MCV5.Models
{
    public class Student : ApplicationUser
    {
        //Student identity number
        [Required]
        [Key]
        public string StudentCode { get; set; }
        public DateTime? EnrollmentDate { get; set; }

        public ICollection<StudentClass> StudentClass { get; set; }
    }
}